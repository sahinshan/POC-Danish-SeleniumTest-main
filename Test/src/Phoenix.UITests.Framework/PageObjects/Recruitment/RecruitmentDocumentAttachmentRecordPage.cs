using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.QualityAndCompliance
{
    public class RecruitmentDocumentAttachmentRecordPage : CommonMethods
    {
        public RecruitmentDocumentAttachmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By RecruitmentDocumentAttachmentRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=complianceitemattachment&')]");


        readonly By pageHeader = By.XPath("//h1");
        readonly By BackButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By additionalToolbarElementsButton = By.Id("CWToolbarMenu");
        readonly By RelatedItemMenu = By.XPath("//a[@class='nav-link dropdown-toggle']");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        
        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

        #endregion

        #region Recruitment Document Attachment General Fields

        readonly By ComplianceItem_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_complianceitemid']//span[@class='mandatory']");
        readonly By ComplianceItem_LinkText = By.Id("CWField_complianceitemid_Link");
        readonly By ComplianceItem_Lookup = By.Id("CWLookupBtn_complianceitemid");

        readonly By ResponsibleTeam_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_ownerid']//span[@class='mandatory']");
        readonly By ResponsibleTeam_LinkText = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_Lookup = By.Id("CWLookupBtn_ownerid");

        readonly By File_Field = By.Id("CWControlHolder_fileid");

        readonly By Name_Field = By.Id("CWField_title");
        readonly By file_Field = By.Id("CWField_fileid");
        readonly By attached_FileName_Field = By.Id("CWField_fileid_FileLink");
        readonly By uploadNewFile_Button = By.Id("CWField_fileid_Upload");
        readonly By upload_Button = By.Id("CWField_fileid_UploadButton");

        readonly By Name_NotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_title']/label/span");
        readonly By File_NotificationErrorLabel = By.XPath("//*[@id='CWField_fileid_FileSection']/label/span");

       

        #endregion

        public RecruitmentDocumentAttachmentRecordPage WaitForRecruitmentDocumentAttachmentRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(RecruitmentDocumentAttachmentRecordIFrame);
            SwitchToIframe(RecruitmentDocumentAttachmentRecordIFrame);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(deleteRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            ScrollToElement(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            ScrollToElement(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            ScrollToElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            ScrollToElement(BackButton);
            Click(BackButton);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            ScrollToElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage NavigateToDetailsTab()
        {
            WaitForElementToBeClickable(DetailsTab);
            ScrollToElement(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage InsertName(string Text)
        {
            WaitForElementVisible(Name_Field);
            SendKeys(Name_Field, Text);
            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage UploadRecruitmentDocumentAttachmentFile(string FilePath)
        {
            WaitForElement(file_Field);
            ScrollToElement(file_Field);
            SendKeys(file_Field, FilePath);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ValidateRecruitmentDocumentAttachmentRecordPageTitle(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            ValidateElementText(pageHeader, "Recruitment Document Attachment:\r\n"+ExpectedText);
            return this;
        }
        public RecruitmentDocumentAttachmentRecordPage ValidateNameFieldByNotificationMessageText(String ExpectedText)
        {
            WaitForElementVisible(Name_NotificationErrorLabel);
            ValidateElementTextContainsText(Name_NotificationErrorLabel, ExpectedText);
            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ValidateFileFieldByNotificationMessageText(String ExpectedText)
        {
            WaitForElementVisible(File_NotificationErrorLabel);
            ValidateElementTextContainsText(File_NotificationErrorLabel, ExpectedText);
            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ValidateComplianceItemName(string ExpectedText)
        {
            WaitForElementVisible(ComplianceItem_LinkText);
            ScrollToElement(ComplianceItem_LinkText);
            ValidateElementText(ComplianceItem_LinkText, ExpectedText);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ValidateResponsibleTeamName(string ExpectedText)
        {
            WaitForElementVisible(ResponsibleTeam_LinkText);
            ScrollToElement(ResponsibleTeam_LinkText);
            ValidateElementText(ResponsibleTeam_LinkText, ExpectedText);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ValidateNameValue(string ExpectedText)
        {
            WaitForElementVisible(Name_Field);
            ScrollToElement(Name_Field);
            ValidateElementValue(Name_Field, ExpectedText);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ValidateAttachedFileName(string ExpectedText)
        {
            WaitForElementVisible(attached_FileName_Field);
            ScrollToElement(attached_FileName_Field);
            ValidateElementTextContainsText(attached_FileName_Field, ExpectedText);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ClickUploadNewFileButton()
        {
            WaitForElementToBeClickable(uploadNewFile_Button);
            ScrollToElement(uploadNewFile_Button);
            Click(uploadNewFile_Button);

            return this;
        }

        public RecruitmentDocumentAttachmentRecordPage ClickUploadButton()
        {
            WaitForElementToBeClickable(upload_Button);
            ScrollToElement(upload_Button);
            Click(upload_Button);

            return this;
        }

    }
}
