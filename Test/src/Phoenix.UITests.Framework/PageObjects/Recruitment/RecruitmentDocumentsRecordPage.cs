using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.QualityAndCompliance
{
    public class RecruitmentDocumentsRecordPage : CommonMethods
    {
        public RecruitmentDocumentsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ComplianceRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'editpage.aspx?type=compliance&')]");

        readonly By ApplicantRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=applicant&')]");
        readonly By Applicant_ComplianceFrame = By.XPath("//iframe[contains(@id,'iframe_')][contains(@src,'type=compliance&')]");
        readonly By pageHeaderTitle = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By pageHeader = By.XPath("//h1[@title='Recruitment Document: New']");
        readonly By BackButton = By.XPath("//button[@title = 'Back']");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By RelatedItemMenu = By.XPath("//a[@class='nav-link dropdown-toggle']");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By Attachment_SubMenuItem = By.Id("CWNavItem_Attachments");
        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");
        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

        readonly By additionalToolbarElementsButton = By.Id("CWToolbarMenu");
        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");
        readonly By complianceItem_NotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_complianceitemid']/label/span");
        readonly By DocumentManagement_SubMenuItem = By.Id("CWNavItem_ComplianceManagement");
        readonly By DocumentManagement_SubMenuItem_Icon = By.XPath("//*[@id='CWNavItem_ComplianceManagement']/img[contains(@src,'resourcefile/ICON_h1_recruitment_document_management.png')]");

        #endregion

        #region Recruitment Document General Fields

        readonly By Regarding_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_regardingid']//span[@class='mandatory']");
        readonly By Regarding_LinkField = By.Id("CWField_regardingid_Link");
        readonly By Regarding_Lookup = By.Id("CWLookupBtn_regardingid");

        readonly By ComplianceItem_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_complianceitemid']//span[@class='mandatory']");
        readonly By ComplianceItem_LinkField = By.Id("CWField_complianceitemid_Link");
        readonly By ComplianceItem_Lookup = By.Id("CWLookupBtn_complianceitemid");

        readonly By ResponsibleTeam_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_ownerid']//span[@class='mandatory']");
        readonly By ResponsibleTeam_LinkField = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_Lookup = By.Id("CWLookupBtn_ownerid");

        readonly By Status_Field = By.Id("CWField_recruitmentdocumentstatusid");
        readonly By VariationField_Label = By.XPath("//*[@id = 'CWLabelHolder_additionalattributeid']/label");
        readonly By Variation_Field = By.Id("CWField_additionalattributeid");

        #endregion

        #region Document Details Fields
        readonly By ReferenceNumber_Field = By.Id("CWField_referencenumber");
        readonly By RefereeName_Field = By.Id("CWField_refereename");

        readonly By RefereeAddress_Field = By.Id("CWField_refereeaddress");
        readonly By RefereePhone_Field = By.Id("CWField_refereephone");
        readonly By RefereeEmail_Field = By.Id("CWField_refereeemail");

        readonly By RefereePhoneFieldValidation_ErrorMessage = By.XPath("//li[@id = 'CWControlHolder_refereephone']//span");
        readonly By RefereeEmailFieldValidation_ErrorMessage = By.XPath("//li[@id = 'CWControlHolder_refereeemail']//span");

        #endregion

        #region Date Details Fields

        readonly By RequestedDate_Field = By.Id("CWField_requesteddate");
        readonly By CompletedDate_Field = By.Id("CWField_completeddate");
        readonly By ValidFromDate_Field = By.Id("CWField_validfromdate");
        readonly By ValidToDate_Field = By.Id("CWField_validtodate");

        readonly By RequestedDate_DatePicker = By.Id("CWField_requesteddate_DatePicker");
        readonly By CompletedDate_DatePicker = By.Id("CWField_completeddate_DatePicker");
        readonly By ValidFromDate_DatePicker = By.Id("CWField_validfromdate_DatePicker");
        readonly By ValidToDate_DatePicker = By.Id("CWField_validtodate_DatePicker");
        readonly By selectedDate = By.XPath("//td[@class = ' ui-datepicker-days-cell-over  ui-datepicker-today']/a[contains(@class , 'ui-state-highlight')]");
        readonly By selectedMonth = By.XPath("//select[@data-handler='selectMonth']");
        readonly By selectedYear = By.XPath("//select[@data-handler='selectYear']");

        readonly By RequestedBy_MandatoryField = By.XPath("//*[@id='CWLabelHolder_requestedbyid']//span[@class='mandatory']");
        readonly By CompletedBy_MandatoryField = By.XPath("//*[@id='CWLabelHolder_completedbyid']//span[@class='mandatory']");
        readonly By RequestedBy_LookupButton = By.Id("CWLookupBtn_requestedbyid");
        readonly By RequestedBy_LinkField = By.Id("CWField_requestedbyid_Link");
        readonly By CompletedBy_LookupButton = By.Id("CWLookupBtn_completedbyid");
        readonly By CompletedBy_LinkField = By.Id("CWField_completedbyid_Link");

        readonly By ValidToDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_validtodate']//span[@class='mandatory']");

        #endregion

        #region Override Details Fields
        readonly By OverrideDetailsSectionHeader = By.XPath("//*[@id = 'CWSection_OverrideDetails']//div/span");
        readonly By OverrideDate_Field = By.Id("CWField_overridedate");
        readonly By OverrideBy_LookupButton = By.Id("CWLookupBtn_overridebyid");
        readonly By OverrideBy_LinkField = By.Id("CWField_overridebyid_Link");
        readonly By OverrideReason_Field = By.Id("CWField_overridereason");

        #endregion

        #region Recruitment Document Attachments Area

        readonly By RecruitmentDocumentAttachmentItems_IFrame = By.Id("CWIFrame_RecruitmentDocumentAttachmentsItems");
        readonly By RecruitmentDocumentAttachements_GridHeader = By.XPath("//h1[text() = 'Recruitment Document Attachments']");

        #endregion

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        By recordCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        #region Recruitment Documents Management Area
        
        readonly By ComplianceManagement_IFrame = By.Id("CWIFrame_ComplianceManagement");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        
        #endregion

        public RecruitmentDocumentsRecordPage WaitForRecruitmentDocumentsRecordPageToLoad()
        {
            SwitchToDefaultFrame();
            
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(ComplianceRecordIFrame);
            SwitchToIframe(ComplianceRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public RecruitmentDocumentsRecordPage WaitForRecruitmentDocumentsRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ComplianceRecordIFrame);
            SwitchToIframe(ComplianceRecordIFrame);


            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public RecruitmentDocumentsRecordPage WaitForRecruitmentDocumentsRecordPageToLoadForApplicant()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ApplicantRecordIFrame);
            SwitchToIframe(ApplicantRecordIFrame);

            WaitForElement(Applicant_ComplianceFrame);
            SwitchToIframe(Applicant_ComplianceFrame);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public RecruitmentDocumentsRecordPage WaitForRecruitmentDocumentsAttachmentsAreaToLoad()
        {
            WaitForElement(RecruitmentDocumentAttachmentItems_IFrame);
            SwitchToIframe(RecruitmentDocumentAttachmentItems_IFrame);

            WaitForElement(RecruitmentDocumentAttachements_GridHeader);
            WaitForElement(NewRecordButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRecruitmentDocumentAttachementGridVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RecruitmentDocumentAttachements_GridHeader);
                ScrollToElement(RecruitmentDocumentAttachements_GridHeader);
                Assert.IsTrue(GetElementVisibility(RecruitmentDocumentAttachements_GridHeader));
            }
            else
            {
                WaitForElementNotVisible(RecruitmentDocumentAttachements_GridHeader, 3);
                Assert.IsFalse(GetElementVisibility(RecruitmentDocumentAttachements_GridHeader));
            }

            return this;
        }

        public RecruitmentDocumentsRecordPage WaitForRecruitmentDocumentsManagementAreaToLoad()
        {
            WaitForElement(ComplianceManagement_IFrame);
            SwitchToIframe(ComplianceManagement_IFrame);

            WaitForElement(NewRecordButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickRegardingLookupButton()
        {
            WaitForElementToBeClickable(Regarding_Lookup);
            Click(Regarding_Lookup);
            return this;
        }

        public RecruitmentDocumentsRecordPage ClickMenuButton()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickRelatedItemsLeftMenu()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            return this;
        }

        public RecruitmentDocumentsRecordPage NavigateToAttachmentsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(Attachment_SubMenuItem);
            Click(Attachment_SubMenuItem);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            ScrollToElement(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            ScrollToElement(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            ScrollToElement(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            ScrollToElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            ScrollToElement(BackButton);
            Click(BackButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            ScrollToElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickComplianceItemLookupButton()
        {
            WaitForElementToBeClickable(ComplianceItem_Lookup);
            ScrollToElement(ComplianceItem_Lookup);
            Click(ComplianceItem_Lookup);

            return this;
        }

        public RecruitmentDocumentsRecordPage SelectVariationOption(string TextToSelect)
        {
            WaitForElement(VariationField_Label);
            ScrollToElement(VariationField_Label);
            WaitForElementVisible(Variation_Field);
            ScrollToElement(VariationField_Label);
            SelectPicklistElementByText(Variation_Field, TextToSelect);
            return this;
        }

        public RecruitmentDocumentsRecordPage ClickRequestedByLookupButton()
        {
            WaitForElementToBeClickable(RequestedBy_LookupButton);
            ScrollToElement(RequestedBy_LookupButton);
            Click(RequestedBy_LookupButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickCompletedByLookupButton()
        {
            WaitForElementToBeClickable(CompletedBy_LookupButton);
            ScrollToElement(CompletedBy_LookupButton);
            Click(CompletedBy_LookupButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage InsertRequestedDateValue(string Date)
        {
            WaitForElementVisible(RequestedDate_Field);
            ScrollToElement(RequestedDate_Field);
            SendKeys(RequestedDate_Field, Date);
            SendKeysWithoutClearing(RequestedDate_Field, Keys.Tab);

            return this;
        }

        public RecruitmentDocumentsRecordPage InsertCompletedDateValue(string Date)
        {
            WaitForElementVisible(CompletedDate_Field);
            ScrollToElement(CompletedDate_Field);
            SendKeys(CompletedDate_Field, Date);
            SendKeysWithoutClearing(CompletedDate_Field, Keys.Tab);

            return this;
        }

        public RecruitmentDocumentsRecordPage InsertValidFromDateValue(string Date)
        {
            WaitForElementVisible(ValidFromDate_Field);
            ScrollToElement(ValidFromDate_Field);
            SendKeys(ValidFromDate_Field, Date);
            SendKeysWithoutClearing(ValidFromDate_Field, Keys.Tab);

            return this;
        }

        public RecruitmentDocumentsRecordPage InsertValidToDateValue(string Date)
        {
            WaitForElementVisible(ValidToDate_Field);
            ScrollToElement(ValidToDate_Field);
            SendKeys(ValidToDate_Field, Date);
            SendKeysWithoutClearing(ValidToDate_Field, Keys.Tab);

            return this;
        }

        public RecruitmentDocumentsRecordPage InsertReferenceNumber(string TextToInsert)
        {
            WaitForElementVisible(ReferenceNumber_Field);
            ScrollToElement(ReferenceNumber_Field);
            SendKeys(ReferenceNumber_Field, TextToInsert);

            return this;
        }

        public RecruitmentDocumentsRecordPage InsertRefereeName(string TextToInsert)
        {
            WaitForElementVisible(RefereeName_Field);
            ScrollToElement(RefereeName_Field);
            SendKeys(RefereeName_Field, TextToInsert);

            return this;
        }

        public RecruitmentDocumentsRecordPage InsertRefereePhone(string TextToInsert)
        {
            WaitForElementVisible(RefereePhone_Field);
            ScrollToElement(RefereePhone_Field);
            SendKeys(RefereePhone_Field, TextToInsert);

            return this;
        }

        public RecruitmentDocumentsRecordPage InsertRefereeAddress(string TextToInsert)
        {
            WaitForElementVisible(RefereeAddress_Field);
            ScrollToElement(RefereeAddress_Field);
            SendKeys(RefereeAddress_Field, TextToInsert);

            return this;
        }

        public RecruitmentDocumentsRecordPage InsertRefereeEmail(string TextToInsert)
        {
            WaitForElementVisible(RefereeEmail_Field);
            ScrollToElement(RefereeEmail_Field);
            SendKeys(RefereeEmail_Field, TextToInsert);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickRequestedDateCalendar()
        {
            WaitForElementToBeClickable(RequestedDate_DatePicker);
            ScrollToElement(RequestedDate_DatePicker);
            Click(RequestedDate_DatePicker);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickCompletedDateCalendar()
        {
            WaitForElementToBeClickable(CompletedDate_DatePicker);
            ScrollToElement(CompletedDate_DatePicker);
            Click(CompletedDate_DatePicker);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickValidFromDateCalendar()
        {
            WaitForElementToBeClickable(ValidFromDate_DatePicker);
            ScrollToElement(ValidFromDate_DatePicker);
            Click(ValidFromDate_DatePicker);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickValidToDateCalendar()
        {
            WaitForElementToBeClickable(ValidToDate_DatePicker);
            ScrollToElement(ValidToDate_DatePicker);
            Click(ValidToDate_DatePicker);

            return this;
        }

        public RecruitmentDocumentsRecordPage InsertOverrideDateValue(string Date)
        {
            WaitForElement(OverrideDetailsSectionHeader);
            ScrollToElement(OverrideDetailsSectionHeader);
            WaitForElementVisible(OverrideDate_Field);
            ScrollToElement(OverrideDate_Field);
            SendKeys(OverrideDate_Field, Date);
            SendKeysWithoutClearing(OverrideDate_Field, Keys.Tab);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickOverrideByLookupButton()
        {
            WaitForElement(OverrideDetailsSectionHeader);
            ScrollToElement(OverrideDetailsSectionHeader);
            WaitForElementToBeClickable(OverrideBy_LookupButton);
            ScrollToElement(OverrideBy_LookupButton);
            Click(OverrideBy_LookupButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage InsertOverrideReason(string TextToInsert)
        {
            WaitForElementVisible(OverrideReason_Field);
            ScrollToElement(OverrideReason_Field);
            SendKeys(OverrideReason_Field, TextToInsert);

            return this;
        }

        public RecruitmentDocumentsRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(deleteRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ClickCreateNewRecruitmentDocumentSubRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            ScrollToElement(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage SelectRecruitmentDocumentAttachmentsRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordCheckBox(RecordId));
            ScrollToElement(recordCheckBox(RecordId));
            Click(recordCheckBox(RecordId));

            return this;
        }

        public RecruitmentDocumentsRecordPage OpenRecruitmentDocumentPageSubRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            ScrollToElement(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateNotificationMessageText(String ExpectedText)
        {
            WaitForElementVisible(notificationMessageArea);
            ScrollToElement(notificationMessageArea);
            ValidateElementTextContainsText(notificationMessageArea, ExpectedText);
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateComplianceItemFieldNotificationMessageText(String ExpectedText)
        {
            WaitForElementVisible(complianceItem_NotificationErrorLabel);
            ScrollToElement(complianceItem_NotificationErrorLabel);
            ValidateElementByTitle(complianceItem_NotificationErrorLabel, ExpectedText);
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRecruitmentStatus_Field_Disabled(bool Disabled)
        {
            WaitForElementVisible(Status_Field);

            if (Disabled)
                ValidateElementDisabled(Status_Field);
            else
                ValidateElementNotDisabled(Status_Field);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRequestedByFieldDisabled(bool Disabled)
        {
            if (Disabled)
                ValidateElementDisabled(RequestedBy_LookupButton);
            else
                ValidateElementNotDisabled(RequestedBy_LookupButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateCompletedByFieldDisabled(bool Disabled)
        {
            if (Disabled)
                ValidateElementDisabled(CompletedBy_LookupButton);
            else
                ValidateElementNotDisabled(CompletedBy_LookupButton);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRegardingApplicantName(string ExpectedText)
        {
            WaitForElementVisible(Regarding_LinkField);
            ScrollToElement(Regarding_LinkField);
            ValidateElementText(Regarding_LinkField, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateResponsibleTeamName(string ExpectedText)
        {
            WaitForElementVisible(ResponsibleTeam_LinkField);
            ScrollToElement(ResponsibleTeam_LinkField);
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateStatusSelectedText(string ExpectedText)
        {
            WaitForElementVisible(Status_Field);
            ScrollToElement(Status_Field);
            ValidatePicklistSelectedText(Status_Field, ExpectedText);
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateVariationSelectedText(string ExpectedText)
        {
            WaitForElementVisible(Variation_Field);
            ScrollToElement(VariationField_Label);            
            ValidatePicklistSelectedText(Variation_Field, ExpectedText);
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateVariationFieldContainsOption(string ExpectedText)
        {
            WaitForElement(VariationField_Label);
            ScrollToElement(VariationField_Label);
            WaitForElementVisible(Variation_Field);
            ValidatePicklistContainsElementByText(Variation_Field, ExpectedText);
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateVariationFieldDoesNotContainOption(string ExpectedText)
        {
            WaitForElement(VariationField_Label);
            ScrollToElement(VariationField_Label);
            WaitForElementVisible(Variation_Field);
            ValidatePicklistDoesNotContainsElementByText(Variation_Field, ExpectedText);
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateComplianceItemName(string ExpectedText)
        {
            WaitForElementVisible(ComplianceItem_LinkField);
            ScrollToElement(ComplianceItem_LinkField);
            ValidateElementText(ComplianceItem_LinkField, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRequestedDateField(string ExpectedText)
        {
            WaitForElementVisible(RequestedDate_Field);
            ScrollToElement(RequestedDate_Field);
            ValidateElementValue(RequestedDate_Field, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateCompletedDateField(string ExpectedText)
        {
            WaitForElementVisible(CompletedDate_Field);
            ScrollToElement(CompletedDate_Field);
            ValidateElementValue(CompletedDate_Field, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateValidFromDateField(string ExpectedText)
        {
            WaitForElementVisible(ValidFromDate_Field);
            ScrollToElement(ValidFromDate_Field);
            ValidateElementValue(ValidFromDate_Field, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateValidToDateField(string ExpectedText)
        {
            WaitForElementVisible(ValidToDate_Field);
            ScrollToElement(ValidToDate_Field);
            ValidateElementValue(ValidToDate_Field, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateOverrideDateField(string ExpectedText)
        {
            WaitForElementVisible(OverrideDate_Field);
            ScrollToElement(OverrideDate_Field);
            ValidateElementValue(OverrideDate_Field, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateOverrideByField(string ExpectedText)
        {
            WaitForElementVisible(OverrideBy_LinkField);
            ScrollToElement(OverrideBy_LinkField);
            ValidateElementByTitle(OverrideBy_LinkField, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateOverrideReasonField(string ExpectedText)
        {
            WaitForElementVisible(OverrideReason_Field);
            ScrollToElement(OverrideReason_Field);
            ValidateElementValue(OverrideReason_Field, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateCompletedDateFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(CompletedDate_Field);
            ScrollToElement(CompletedDate_Field);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(CompletedDate_Field);
            }
            else
            {
                ValidateElementNotDisabled(CompletedDate_Field);
            }

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateValidFromDateFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(ValidFromDate_Field);
            ScrollToElement(ValidFromDate_Field);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(ValidFromDate_Field);
            }
            else
            {
                ValidateElementNotDisabled(ValidFromDate_Field);
            }

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateValidToDateFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(ValidToDate_Field);
            ScrollToElement(ValidToDate_Field);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(ValidToDate_Field);
            }
            else
            {
                ValidateElementNotDisabled(ValidToDate_Field);
            }

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRequestedByLinkText(string ExpectedText)
        {
            WaitForElementVisible(RequestedBy_LinkField);
            ScrollToElement(RequestedBy_LinkField);
            ValidateElementText(RequestedBy_LinkField, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateCompletedByLinkText(string ExpectedText)
        {
            WaitForElementVisible(CompletedBy_LinkField);
            ScrollToElement(CompletedBy_LinkField);
            ValidateElementText(CompletedBy_LinkField, ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateSelectedDate(string expectedDate)
        {
            WaitForElementVisible(selectedDate);
            ScrollToElement(selectedDate);
            Assert.AreEqual(expectedDate, GetElementText(selectedDate));
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateSelectedMonth(string expectedMonth)
        {
            WaitForElementVisible(selectedMonth);
            ScrollToElement(selectedMonth);
            ValidatePicklistSelectedText(selectedMonth, expectedMonth);
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateSelectedYear(string expectedYear)
        {
            WaitForElementVisible(selectedYear);
            ScrollToElement(selectedYear);
            ValidatePicklistSelectedText(selectedYear, expectedYear);
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRequestedByMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RequestedBy_MandatoryField);
                ScrollToElement(RequestedBy_MandatoryField);
                Assert.IsTrue(GetElementVisibility(RequestedBy_MandatoryField));
            }
            else
            {
                WaitForElementNotVisible(RequestedBy_MandatoryField, 3);
                Assert.IsFalse(GetElementVisibility(RequestedBy_MandatoryField));
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateCompletedByMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CompletedBy_MandatoryField);
                ScrollToElement(CompletedBy_MandatoryField);
                Assert.IsTrue(GetElementVisibility(CompletedBy_MandatoryField));
            }
            else
            {
                WaitForElementNotVisible(CompletedBy_MandatoryField, 3);
                Assert.IsFalse(GetElementVisibility(CompletedBy_MandatoryField));
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateReferenceNumberFieldValue(string expectedText)
        {
            WaitForElementVisible(ReferenceNumber_Field);
            ScrollToElement(ReferenceNumber_Field);
            ValidateElementValue(ReferenceNumber_Field, expectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRefereeNameFieldValue(string expectedText)
        {
            WaitForElementVisible(RefereeName_Field);
            ScrollToElement(RefereeName_Field);
            ValidateElementValue(RefereeName_Field, expectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRefereePhoneFieldValue(string expectedText)
        {
            WaitForElementVisible(RefereePhone_Field);
            ScrollToElement(RefereePhone_Field);
            ValidateElementValue(RefereePhone_Field, expectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRefereeAddressFieldValue(string expectedText)
        {
            WaitForElementVisible(RefereeAddress_Field);
            ScrollToElement(RefereeAddress_Field);
            ValidateElementValue(RefereeAddress_Field, expectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRefereeEmailFieldValue(string expectedText)
        {
            WaitForElementVisible(RefereeEmail_Field);
            ScrollToElement(RefereeEmail_Field);
            ValidateElementValue(RefereeEmail_Field, expectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRefereePhoneFieldNotificationMessageText(string expectedText)
        {
            WaitForElementVisible(RefereePhoneFieldValidation_ErrorMessage);
            ScrollToElement(RefereePhoneFieldValidation_ErrorMessage);
            ValidateElementByTitle(RefereePhoneFieldValidation_ErrorMessage, expectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRefereeEmailFieldNotificationMessageText(string expectedText)
        {
            WaitForElementVisible(RefereeEmailFieldValidation_ErrorMessage);
            ScrollToElement(RefereeEmailFieldValidation_ErrorMessage);
            ValidateElementByTitle(RefereeEmailFieldValidation_ErrorMessage, expectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRecruitmentDocumentPageSubRecordIsPresent(string RecordId)
        {            
            WaitForElementToBeClickable(recordRow(RecordId));
            ScrollToElement(recordRow(RecordId));

            bool isRecordPresent = GetElementVisibility(recordRow(RecordId));
            Assert.IsTrue(isRecordPresent);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRecruitmentDocumentPageSubRecordIsNotPresent(string RecordId)
        {

            bool isRecordPresent = GetElementVisibility(recordRow(RecordId));
            Assert.IsFalse(isRecordPresent);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateAttachmentLeftSubMenuItemAvailable(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Attachment_SubMenuItem);
                ScrollToElement(Attachment_SubMenuItem);
                Assert.IsTrue(GetElementVisibility(Attachment_SubMenuItem));
            }
            else
            {
                WaitForElementNotVisible(Attachment_SubMenuItem, 3);
                Assert.IsFalse(GetElementVisibility(Attachment_SubMenuItem));
            }

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateValidToDate_MandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ValidToDate_Field);
            ScrollToElement(ValidToDate_Field);
            Assert.AreEqual(ExpectVisible, GetElementVisibility(ValidToDate_MandatoryField));

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidatePageHeaderTitle(string ExpectedText)
        {
            WaitForElementVisible(pageHeaderTitle);
            ValidateElementText(pageHeaderTitle, "Recruitment Document:\r\n" + ExpectedText);

            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateReferenceNumberFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ReferenceNumber_Field);
            }
            else
            {
                WaitForElementNotVisible(ReferenceNumber_Field, 3);               
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRefereeNameFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RefereeName_Field);
            }
            else
            {
                WaitForElementNotVisible(RefereeName_Field, 3);
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRefereePhoneFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RefereePhone_Field);
            }
            else
            {
                WaitForElementNotVisible(RefereePhone_Field, 3);
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRefereeAddressFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RefereeAddress_Field);
            }
            else
            {
                WaitForElementNotVisible(RefereeAddress_Field, 3);
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRefereeEmailFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RefereeEmail_Field);
            }
            else
            {
                WaitForElementNotVisible(RefereeEmail_Field, 3);
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRequestedDateFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RequestedDate_Field);
            }
            else
            {
                WaitForElementNotVisible(RequestedDate_Field, 3);
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRequestedByLookupFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RequestedBy_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(RequestedBy_LookupButton, 3);
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateCompletedDateFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CompletedDate_Field);
            }
            else
            {
                WaitForElementNotVisible(CompletedDate_Field, 3);
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateCompletedByLookupFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CompletedBy_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(CompletedBy_LookupButton, 3);
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateValidFromDateFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ValidFromDate_Field);
            }
            else
            {
                WaitForElementNotVisible(ValidFromDate_Field, 3);
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateValidToDateFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ValidToDate_Field);
            }
            else
            {
                WaitForElementNotVisible(ValidToDate_Field, 3);
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateOverrideDetailsSectionFieldsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(OverrideDetailsSectionHeader);
                WaitForElementVisible(OverrideDate_Field);
                WaitForElementVisible(OverrideBy_LookupButton);
                WaitForElementVisible(OverrideReason_Field);
            }
            else
            {
                WaitForElementNotVisible(OverrideDetailsSectionHeader, 3);
                WaitForElementNotVisible(OverrideDate_Field, 3);
                WaitForElementNotVisible(OverrideBy_LookupButton, 3);
                WaitForElementNotVisible(OverrideReason_Field, 3);
            }
            return this;
        }

        public RecruitmentDocumentsRecordPage ValidateRelatedItems_DocumentManagementIcon()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementVisible(DocumentManagement_SubMenuItem);
            WaitForElementVisible(DocumentManagement_SubMenuItem_Icon);

            return this;
        }

    }
}
