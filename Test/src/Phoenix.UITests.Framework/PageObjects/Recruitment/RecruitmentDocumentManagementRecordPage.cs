using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Recruitment
{
    public class RecruitmentDocumentManagementRecordPage : CommonMethods
    {
        public RecruitmentDocumentManagementRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ComplianceManagementRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=compliancemanagement&')]");

        readonly By pageHeader = By.XPath("//h1");
        readonly By BackButton = By.XPath("//button[@title = 'Back']");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By RelatedItemMenu = By.XPath("//a[@class='nav-link dropdown-toggle']");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");
        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");
        readonly By additionalToolbarElementsButton = By.Id("CWToolbarMenu");
        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");
        readonly By notificationErrorLabel = By.XPath("//*[@id='CWControlHolder_complianceitemid']/label/span");

        #endregion

        #region Recruitment Document Management General Fields

        readonly By document_ComplianceItem_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_complianceid']//span[@class='mandatory']");
        readonly By document_ComplianceItemField_Link = By.Id("CWField_complianceid_Link");
        readonly By document_ComplianceItemField_Lookup = By.Id("CWLookupBtn_complianceid");

        readonly By ResponsibleTeam_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_ownerid']//span[@class='mandatory']");
        readonly By ResponsibleTeam_LinkField = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_Lookup = By.Id("CWLookupBtn_ownerid");

        readonly By ChasedDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_chaseddate']//span[@class='mandatory']");
        readonly By ChasedDate_Field = By.Id("CWField_chaseddate");
        readonly By ChasedDate_DatePicker = By.Id("CWField_chaseddate_DatePicker");
        readonly By selectedDate = By.XPath("//td[@class = ' ui-datepicker-days-cell-over  ui-datepicker-today']/a[contains(@class , 'ui-state-highlight')]");
        readonly By selectedMonth = By.XPath("//select[@data-handler='selectMonth']");
        readonly By selectedYear = By.XPath("//select[@data-handler='selectYear']");
                   
        readonly By ChasedBy_LinkField = By.Id("CWField_chasedbyid_Link");
        readonly By ChasedByField_Lookup = By.Id("CWLookupBtn_chasedbyid");
        readonly By ChasedBy_MandatoryField = By.XPath("//*[@id='CWLabelHolder_chasedbyid']//span[@class='mandatory']");

        readonly By Outcome_LinkField = By.Id("CWField_requirementlastchasedoutcomeid_Link");
        readonly By OutcomeField_Lookup = By.Id("CWLookupBtn_requirementlastchasedoutcomeid");
        readonly By Outcome_MandatoryField = By.XPath("//*[@id='CWLabelHolder_requirementlastchasedoutcomeid']//span[@class='mandatory']");

        readonly By Notes_TextAreaField = By.Id("CWField_notes");

        #endregion

        public RecruitmentDocumentManagementRecordPage WaitForRecruitmentDocumentManagementRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ComplianceManagementRecordIFrame);
            SwitchToIframe(ComplianceManagementRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage WaitForRecruitmentDocumentManagementRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(deleteRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        #region Populate Values Methods

        public RecruitmentDocumentManagementRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            ScrollToElement(BackButton);
            Click(BackButton);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            ScrollToElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            ScrollToElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage InsertChasedDateValue(string Date)
        {
            WaitForElementVisible(ChasedDate_Field);
            ScrollToElement(ChasedDate_Field);
            SendKeys(ChasedDate_Field, Date);
            SendKeysWithoutClearing(ChasedDate_Field, Keys.Tab);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ClickChasedDateCalendar()
        {
            WaitForElementToBeClickable(ChasedDate_DatePicker);
            ScrollToElement(ChasedDate_DatePicker);
            Click(ChasedDate_DatePicker);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_Lookup);
            ScrollToElement(ResponsibleTeam_Lookup);
            Click(ResponsibleTeam_Lookup);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ClickChasedByLookupButton()
        {
            WaitForElementToBeClickable(ChasedByField_Lookup);
            ScrollToElement(ChasedByField_Lookup);
            Click(ChasedByField_Lookup);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ClickOutcomeFieldLookupButton()
        {
            WaitForElementToBeClickable(OutcomeField_Lookup);
            ScrollToElement(OutcomeField_Lookup);
            Click(OutcomeField_Lookup);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage InsertNotesText(string TextToInsert)
        {
            WaitForElement(Notes_TextAreaField);
            ScrollToElement(Notes_TextAreaField);
            SendKeys(Notes_TextAreaField, TextToInsert);

            return this;
        }

        #endregion

        #region Validation Methods

        public RecruitmentDocumentManagementRecordPage ValidateDocumentMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(document_ComplianceItem_Mandatory_Field);
                ScrollToElement(document_ComplianceItem_Mandatory_Field);
                Assert.IsTrue(GetElementVisibility(document_ComplianceItem_Mandatory_Field));
            }
            else
            {
                WaitForElementNotVisible(document_ComplianceItem_Mandatory_Field, 3);
                Assert.IsFalse(GetElementVisibility(document_ComplianceItem_Mandatory_Field));
            }
            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateDocumentField(string ExpectedText)
        {
            WaitForElementVisible(document_ComplianceItemField_Link);
            ScrollToElement(document_ComplianceItemField_Link);
            ValidateElementByTitle(document_ComplianceItemField_Link, ExpectedText);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateChasedByMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ChasedBy_MandatoryField);
                ScrollToElement(ChasedBy_MandatoryField);
                Assert.IsTrue(GetElementVisibility(ChasedBy_MandatoryField));
            }
            else
            {
                WaitForElementNotVisible(ChasedBy_MandatoryField, 3);
                Assert.IsFalse(GetElementVisibility(ChasedBy_MandatoryField));
            }
            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateChasedDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ChasedDate_MandatoryField);
                ScrollToElement(ChasedDate_MandatoryField);
                Assert.IsTrue(GetElementVisibility(ChasedDate_MandatoryField));
            }
            else
            {
                WaitForElementNotVisible(ChasedDate_MandatoryField, 3);
                Assert.IsFalse(GetElementVisibility(ChasedDate_MandatoryField));
            }
            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateChasedDateFieldValue(string ExpectedText)
        {
            WaitForElementVisible(ChasedDate_Field);
            ScrollToElement(ChasedDate_Field);
            ValidateElementValue(ChasedDate_Field, ExpectedText);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateResponsibleDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ResponsibleTeam_Mandatory_Field);
                ScrollToElement(ResponsibleTeam_Mandatory_Field);
                Assert.IsTrue(GetElementVisibility(ResponsibleTeam_Mandatory_Field));
            }
            else
            {
                WaitForElementNotVisible(ResponsibleTeam_Mandatory_Field, 3);
                Assert.IsFalse(GetElementVisibility(ResponsibleTeam_Mandatory_Field));
            }
            return this;
        }     

        public RecruitmentDocumentManagementRecordPage ValidateOutcomeMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Outcome_MandatoryField);
                ScrollToElement(Outcome_MandatoryField);
                Assert.IsTrue(GetElementVisibility(Outcome_MandatoryField));
            }
            else
            {
                WaitForElementNotVisible(Outcome_MandatoryField, 3);
                Assert.IsFalse(GetElementVisibility(Outcome_MandatoryField));
            }
            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateChasedByField(string ExpectedText)
        {
            WaitForElementVisible(ChasedBy_LinkField);
            ScrollToElement(ChasedBy_LinkField);
            ValidateElementByTitle(ChasedBy_LinkField, ExpectedText);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateOutcomeField(string ExpectedText)
        {
            WaitForElementVisible(Outcome_LinkField);
            ScrollToElement(Outcome_LinkField);
            ValidateElementByTitle(Outcome_LinkField, ExpectedText);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateSelectedDate(string expectedDate)
        {
            WaitForElementVisible(selectedDate);
            ScrollToElement(selectedDate);
            Assert.AreEqual(expectedDate, GetElementText(selectedDate));
            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateSelectedMonth(string expectedMonth)
        {
            WaitForElementVisible(selectedMonth);
            ScrollToElement(selectedMonth);
            ValidatePicklistSelectedText(selectedMonth, expectedMonth);
            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateSelectedYear(string expectedYear)
        {
            WaitForElementVisible(selectedYear);
            ScrollToElement(selectedYear);
            ValidatePicklistSelectedText(selectedYear, expectedYear);
            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateRecruitmentStatusFieldLookupButtonDisabled(bool ExpectVisible)
        {
            WaitForElementVisible(document_ComplianceItemField_Lookup);

            if (ExpectVisible)
                ValidateElementDisabled(document_ComplianceItemField_Lookup);
            else
                ValidateElementNotDisabled(document_ComplianceItemField_Lookup);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateChasedByFieldLookupButtonDisabled(bool ExpectVisible)
        {
            WaitForElementVisible(ChasedByField_Lookup);

            if (ExpectVisible)
                ValidateElementDisabled(ChasedByField_Lookup);
            else
                ValidateElementNotDisabled(ChasedByField_Lookup);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateResponsibleTeamFieldLookupButtonDisabled(bool ExpectVisible)
        {
            WaitForElementVisible(ResponsibleTeam_Lookup);

            if (ExpectVisible)
                ValidateElementDisabled(ResponsibleTeam_Lookup);
            else
                ValidateElementNotDisabled(ResponsibleTeam_Lookup);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateOutcomeFieldLookupButtonDisabled(bool ExpectVisible)
        {
            WaitForElementVisible(OutcomeField_Lookup);

            if (ExpectVisible)
                ValidateElementDisabled(OutcomeField_Lookup);
            else
                ValidateElementNotDisabled(OutcomeField_Lookup);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateResponsibleTeamName(string ExpectedText)
        {
            WaitForElementVisible(ResponsibleTeam_LinkField);
            ScrollToElement(ResponsibleTeam_LinkField);
            ValidateElementByTitle(ResponsibleTeam_LinkField, ExpectedText);

            return this;            
        }

        public RecruitmentDocumentManagementRecordPage ValidateNotesTextAreaDisabled(bool ExpectedStatus)
        {
            WaitForElementVisible(Notes_TextAreaField);

            if (ExpectedStatus)
                ValidateElementDisabled(Notes_TextAreaField);
            else
                ValidateElementNotDisabled(Notes_TextAreaField);

            return this;
        }

        public RecruitmentDocumentManagementRecordPage ValidateNotesText(string ExpectedText)
        {
            WaitForElementVisible(Notes_TextAreaField);
            ScrollToElement(Notes_TextAreaField);
            string actualNotesText = GetElementText(Notes_TextAreaField);
            Assert.AreEqual(ExpectedText, actualNotesText);

            return this;
        }

        #endregion
    }
}
