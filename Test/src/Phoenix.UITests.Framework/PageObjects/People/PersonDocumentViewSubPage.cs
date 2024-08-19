
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonDocumentViewSubPage : CommonMethods
    {
        public PersonDocumentViewSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWHTMLResourcePanel_IFrame = By.Id("CWUrlPanel_IFrame");
        
        
        readonly By pageTitle = By.XPath("//h1[text()='Document View']");


        #region Top Menu

        readonly By downloadSelectedButton = By.Id("CWDownloadSelected");

        #endregion


        #region Search Area

        readonly By SearchAreaTitle = By.XPath("//*[@id='CWCommonFilterLabel']");

        readonly By SearchArea_DateFromField = By.XPath("//*[@id='CWStartDate']");
        readonly By SearchArea_DateToField = By.XPath("//*[@id='CWEndDate']");
        readonly By SearchArea_ProfessionTypeLinkField = By.XPath("//*[@id='CWFieldProfessionalRoleId_Link']");
        readonly By SearchArea_ProfessionTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_CWFieldProfessionalRoleId']");

        
        readonly By SearchArea_FormFiltersTitle = By.XPath("//*[@id='CWDocumentFilterLabel']");
        readonly By SearchArea_FormDocumentCategoryLinkField = By.XPath("//*[@id='CWDocumentCategoryId_Link']");
        readonly By SearchArea_FormDocumentCategoryLookupButton = By.XPath("//*[@id='CWLookupBtn_CWDocumentCategoryId']");
        readonly By SearchArea_FormDocumentLinkField = By.XPath("//*[@id='CWDocumentId_Link']");
        readonly By SearchArea_FormDocumentLookupButton = By.XPath("//*[@id='CWLookupBtn_CWDocumentId']");

        readonly By SearchArea_AttachmentFiltersTitle = By.XPath("//*[@id='CWAttachmentFilterLabel']");
        readonly By SearchArea_AttachmentTitleField = By.XPath("//*[@id='CWAttachmentTitle']");
        readonly By SearchArea_AttachmentDocumentTypeLinkField = By.XPath("//*[@id='CWAttachmentDocumentTypeId_Link']");
        readonly By SearchArea_AttachmentDocumentTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_CWAttachmentDocumentTypeId']");
        readonly By SearchArea_AttachmentDocumentSubTypeLinkField = By.XPath("//*[@id='CWAttachmentDocumentSubTypeId_Link']");
        readonly By SearchArea_AttachmentDocumentSubTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_CWAttachmentDocumentSubTypeId']");

        readonly By SearchArea_LetterFiltersTitle = By.XPath("//*[@id='CWLetterFilterLabel']");
        readonly By SearchArea_LetterTitleField = By.XPath("//*[@id='CWLetterTitle']");
        
        readonly By SearchArea_SearchButton = By.XPath("//*[@id='CWSearchButton']");
        readonly By SearchArea_ClearFilter = By.XPath("//*[@id='CWClearFiltersButton']");

        #endregion


        #region Results Area

        #region Forms
        readonly By forms_NoRecordsPresentMessage = By.XPath("//*[@id='CWAssessmentFormList']/li/span[text()='No records present']");
        

        readonly By forms_SelectAllCheckBox = By.XPath("//*[@id='CWAssessmentFormListParent']");
        readonly By forms_SelectAllCaseFormsCheckBox = By.XPath("//*[@id='DocCat_caseform']");
        readonly By forms_SelectAllPersonFormsCheckBox = By.XPath("//*[@id='DocCat_personform']");

        readonly By formsExpandButton = By.XPath("//*[@id='CWRelatedRecordsList']/li[1]/div");
        readonly By caseFormExpandButton = By.XPath("//*[@id='CWAssessmentFormList']/li[1]/div");
        readonly By personFormExpandButton = By.XPath("//*[@id='CWAssessmentFormList']/li[2]/div");

        By caseDocumentCheckBox(string DocumentID) => By.Id("AF_" + DocumentID);
        By caseDownloadIcon(string DocumentID) => By.XPath("//a[@title='Download File'][contains(@onclick,'" + DocumentID + "')]");
        By caseLink(string DocumentID) => By.XPath("//a[@for='AF_" + DocumentID + "']");

        #endregion

        #region Attachments
        readonly By attachments_NoRecordsPresentMessage = By.XPath("//*[@id='CWAttachmentList']/li/span[text()='No records present']");

        readonly By attachments_SelectAllCheckBox = By.XPath("//*[@id='CWAttachmentListParent']");
        readonly By attachments_SelectAllAttachmentsForCaseCheckBox = By.XPath("//*[@id='AG_caseattachment']");
        readonly By attachments_AttachmentsForCase_AllAttachedDocumentsCheckBox = By.XPath("//*[@id='DT_62d55830-0466-eb11-a308-005056926fe4_caseattachment']");
        readonly By attachments_AttachmentsForCase_ClinicalChemistryResultsCheckBox = By.XPath("//*[@id='DT_f351b824-70f6-e911-a2c7-005056926fe4_caseattachment']");
        readonly By attachments_SelectAllAttachmentsForPersonCheckBox = By.XPath("//*[@id='AG_personattachment']");
        readonly By attachments_AttachmentsForPerson_AllAttachedDocumentsCheckBox = By.XPath("//*[@id='DT_62d55830-0466-eb11-a308-005056926fe4_personattachment']");
        readonly By attachments_AttachmentsForPerson_ClinicalChemistryResultsCheckBox = By.XPath("//*[@id='DT_f351b824-70f6-e911-a2c7-005056926fe4_personattachment']");

        readonly By attachmentsExpandButton = By.XPath("//*[@id='CWAttachmentListParent']/parent::li/div");

        readonly By attachmentsForCaseExpandButton = By.XPath("//*[@id='AG_caseattachment']/parent::li/div");
        readonly By attachmentsForCase_AllAttachedDocumentsExpandButton = By.XPath("//*[@id='DT_62d55830-0466-eb11-a308-005056926fe4_caseattachment']/parent::li/div");
        readonly By attachmentsForCase_ClinicalChemistryResultsExpandButton = By.XPath("//*[@id='DT_f351b824-70f6-e911-a2c7-005056926fe4_caseattachment']/parent::li/div");

        readonly By attachmentsForPersonExpandButton = By.XPath("//*[@id='AG_personattachment']/parent::li/div");
        readonly By attachmentsForPerson_AllAttachedDocumentsExpandButton = By.XPath("//*[@id='DT_62d55830-0466-eb11-a308-005056926fe4_personattachment']/parent::li/div");
        readonly By attachmentsForPerson_ClinicalChemistryResultsExpandButton = By.XPath("//*[@id='DT_f351b824-70f6-e911-a2c7-005056926fe4_personattachment']/parent::li/div");

        By attachmentDocumentCheckBox(string DocumentID) => By.Id("ATT_" + DocumentID);
        By attachmentDownloadIcon(string FileID) => By.XPath("//a[@title='Download File'][contains(@onclick,'" + FileID + "')]");
        By attachmentLink(string DocumentID) => By.XPath("//a[@for='ATT_" + DocumentID + "']");

        By CaseFormAttachmentRecordLinkPosition(int ExpectedPosition, string RecordID) => By.XPath("//*[@id='CWAttachmentList']/li[1]//ul/li[" + ExpectedPosition + "]/input[@id='ATT_" + RecordID + "']");

        By PersonFormAttachmentRecordLinkPosition(int ExpectedPosition, string RecordID) => By.XPath("//*[@id='CWAttachmentList']/li[2]//ul/li[" + ExpectedPosition + "]/input[@id='ATT_" + RecordID + "']");

        By Person_Case_FormRecordLinkPosition(int ExpectedPosition, string RecordID) => By.XPath("//*[@id='CWAssessmentFormList']/li[1]//ul/li[" + ExpectedPosition + "]/input[@id='AF_" + RecordID + "']");

        #endregion

        #region Letters

        readonly By letters_NoRecordsPresentMessage = By.XPath("//*[@id='CWLetterList']/li/span[text()='No records present']");

        readonly By letters_SelectAllCheckBox = By.XPath("//*[@id='CWLetterListParent']");
                    
        readonly By lettersExpandButton = By.XPath("//*[@id='CWLetterListParent']/parent::li/div");

        By letterDocumentCheckBox(string DocumentID) => By.Id("L_" + DocumentID);
        By letterDownloadIcon(string FileId) => By.XPath("//a[@title='Download File'][contains(@onclick,'" + FileId + "')]");
        By letterLink(string DocumentID) => By.XPath("//a[@for='L_" + DocumentID + "']");

        #endregion

        #endregion


        public PersonDocumentViewSubPage WaitForPersonDocumentViewSubPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWHTMLResourcePanel_IFrame);
            SwitchToIframe(CWHTMLResourcePanel_IFrame);

            WaitForElement(pageTitle);
            WaitForElement(downloadSelectedButton);

            WaitForElement(SearchAreaTitle);

            WaitForElement(SearchArea_DateFromField);
            WaitForElement(SearchArea_DateToField);
            WaitForElement(SearchArea_ProfessionTypeLinkField);
            WaitForElement(SearchArea_ProfessionTypeLookupButton);

            WaitForElement(SearchArea_FormDocumentCategoryLinkField);
            WaitForElement(SearchArea_FormDocumentCategoryLookupButton);
            WaitForElement(SearchArea_FormDocumentLinkField);
            WaitForElement(SearchArea_FormDocumentLookupButton);

            WaitForElement(SearchArea_AttachmentTitleField);
            WaitForElement(SearchArea_AttachmentDocumentTypeLinkField);
            WaitForElement(SearchArea_AttachmentDocumentTypeLookupButton);
            WaitForElement(SearchArea_AttachmentDocumentSubTypeLinkField);
            WaitForElement(SearchArea_AttachmentDocumentSubTypeLookupButton);

            WaitForElement(SearchArea_LetterTitleField);

            WaitForElement(SearchArea_SearchButton);
            WaitForElement(SearchArea_ClearFilter);

            return this;
        }

        public PersonDocumentViewSubPage ClickDownloadSelectedButton()
        {
            Click(downloadSelectedButton);

            //WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }



        #region Search Area

        public PersonDocumentViewSubPage InsertDateFrom(string TextToInsert)
        {
            SendKeys(SearchArea_DateFromField, TextToInsert);

            return this;
        }

        public PersonDocumentViewSubPage InsertDateTo(string TextToInsert)
        {
            SendKeys(SearchArea_DateToField, TextToInsert);

            return this;
        }

        public PersonDocumentViewSubPage ClickProfessionTypeLookupButton()
        {
            Click(SearchArea_ProfessionTypeLookupButton);

            return this;
        }



        public PersonDocumentViewSubPage ClickFormFiltersTitle()
        {
            Click(SearchArea_FormFiltersTitle);

            return this;
        }

        public PersonDocumentViewSubPage ClickAttachmentFiltersTitle()
        {
            Click(SearchArea_AttachmentFiltersTitle);

            return this;
        }

        public PersonDocumentViewSubPage ClickLetterFiltersTitle()
        {
            Click(SearchArea_LetterFiltersTitle);

            return this;
        }



        public PersonDocumentViewSubPage ClickDocumentCategoryLookupButton()
        {
            Click(SearchArea_FormDocumentCategoryLookupButton);

            return this;
        }

        public PersonDocumentViewSubPage ClickDocumentLookupButton()
        {
            Click(SearchArea_FormDocumentLookupButton);

            return this;
        }



        public PersonDocumentViewSubPage InsertAttachmentTitle(string TextToInsert)
        {
            SendKeys(SearchArea_AttachmentTitleField, TextToInsert);

            return this;
        }

        public PersonDocumentViewSubPage ClickDocumentTypeLookupButton()
        {
            Click(SearchArea_AttachmentDocumentTypeLookupButton);

            return this;
        }

        public PersonDocumentViewSubPage ClickDocumentSubTypeLookupButton()
        {
            Click(SearchArea_AttachmentDocumentSubTypeLookupButton);

            return this;
        }



        public PersonDocumentViewSubPage InsertLetterTitleField(string TextToInsert)
        {
            SendKeys(SearchArea_LetterTitleField, TextToInsert);

            return this;
        }



        public PersonDocumentViewSubPage ClickClearFilterButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            Click(SearchArea_ClearFilter);

            return this;
        }

        public PersonDocumentViewSubPage ClickSearchButton()
        {
            Click(SearchArea_SearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        #endregion


        #region Results Area

        #region Forms
        
        public PersonDocumentViewSubPage ValidateFormNoRecordsPresentMessageVisible()
        {
            WaitForElementVisible(forms_NoRecordsPresentMessage);

            return this;
        }


        public PersonDocumentViewSubPage ValidateFormLinkText(string DocumentID, string ExpectedText)
        {
            ValidateElementText(caseLink(DocumentID), ExpectedText);

            return this;
        }

        public PersonDocumentViewSubPage ValidateFormRecordVisible(string DocumentID)
        {
            WaitForElementVisible(caseDocumentCheckBox(DocumentID));
            WaitForElementVisible(caseDownloadIcon(DocumentID));
            WaitForElementVisible(caseLink(DocumentID));

            return this;
        }

        public PersonDocumentViewSubPage ValidateFormRecordNotVisible(string DocumentID)
        {
            WaitForElementNotVisible(caseDocumentCheckBox(DocumentID), 7);
            WaitForElementNotVisible(caseDownloadIcon(DocumentID), 7);
            WaitForElementNotVisible(caseLink(DocumentID), 7);

            return this;
        }



        public PersonDocumentViewSubPage ClickSelectAllFormsCheckBox()
        {
            WaitForElement(forms_SelectAllCheckBox);
            Click(forms_SelectAllCheckBox);

            return this;
        }

        public PersonDocumentViewSubPage ClickSelectAllCaseFormsCheckBox()
        {
            WaitForElement(forms_SelectAllCaseFormsCheckBox);
            Click(forms_SelectAllCaseFormsCheckBox);

            return this;
        }

        public PersonDocumentViewSubPage ClickSelectAllPersonFormsCheckBox()
        {
            WaitForElement(forms_SelectAllPersonFormsCheckBox);
            Click(forms_SelectAllPersonFormsCheckBox);

            return this;
        }



        public PersonDocumentViewSubPage ClickFormsExpandButton()
        {
            WaitForElement(formsExpandButton);
            Click(formsExpandButton);

            return this;
        }

        public PersonDocumentViewSubPage ClickCaseFormExpandButton()
        {
            WaitForElement(caseFormExpandButton);
            Click(caseFormExpandButton);

            return this;
        }

        public PersonDocumentViewSubPage ClickPersonFormExpandButton()
        {
            WaitForElement(personFormExpandButton);
            Click(personFormExpandButton);

            return this;
        }



        public PersonDocumentViewSubPage ClickFormRecordLink(string DocumentID)
        {
            WaitForElementToBeClickable(caseLink(DocumentID));
            Click(caseLink(DocumentID));

            return this;
        }

        public PersonDocumentViewSubPage ClickFormDownloadIcon(string DocumentID)
        {
            WaitForElementToBeClickable(caseDownloadIcon(DocumentID));
            Click(caseDownloadIcon(DocumentID));

            return this;
        }

        public PersonDocumentViewSubPage SelectFormDocument(string DocumentID)
        {
            WaitForElement(caseDocumentCheckBox(DocumentID));
            Click(caseDocumentCheckBox(DocumentID));

            return this;
        }

        #endregion

        #region Attachments

        public PersonDocumentViewSubPage ValidateAttachmentsNoRecordsPresentMessageVisible()
        {
            WaitForElementVisible(attachments_NoRecordsPresentMessage);

            return this;
        }

        public PersonDocumentViewSubPage ValidateAttachmentLinkText(string DocumentID, string ExpectedText)
        {
            ValidateElementText(attachmentLink(DocumentID), ExpectedText);

            return this;
        }

        public PersonDocumentViewSubPage ValidateAttachmentRecordVisible(string DocumentID, string DocumentFileID)
        {
            WaitForElementVisible(attachmentDocumentCheckBox(DocumentID));
            WaitForElementVisible(attachmentDownloadIcon(DocumentFileID));
            WaitForElementVisible(attachmentLink(DocumentID));

            return this;
        }

        public PersonDocumentViewSubPage ValidateAttachmentRecordNotVisible(string DocumentID, string DocumentFileID)
        {
            WaitForElementNotVisible(attachmentDocumentCheckBox(DocumentID), 3);
            WaitForElementNotVisible(attachmentDownloadIcon(DocumentFileID), 3);
            WaitForElementNotVisible(attachmentLink(DocumentID), 3);

            return this;
        }


        public PersonDocumentViewSubPage ClickSelectAllAttachmentsCheckBox()
        {
            WaitForElement(attachments_SelectAllCheckBox);
            Click(attachments_SelectAllCheckBox);

            return this;
        }

        public PersonDocumentViewSubPage ClickSelectAllAttachmentsForCaseCheckBox()
        {
            WaitForElement(attachments_SelectAllAttachmentsForCaseCheckBox);
            Click(attachments_SelectAllAttachmentsForCaseCheckBox);

            return this;
        }
        public PersonDocumentViewSubPage ClickAttachmentsForCase_AllAttachedDocumentsCheckBox()
        {
            WaitForElement(attachments_AttachmentsForCase_AllAttachedDocumentsCheckBox);
            Click(attachments_AttachmentsForCase_AllAttachedDocumentsCheckBox);

            return this;
        }
        public PersonDocumentViewSubPage ClickAttachmentsForCase_ClinicalChemistryResultsCheckBox()
        {
            WaitForElement(attachments_AttachmentsForCase_ClinicalChemistryResultsCheckBox);
            Click(attachments_AttachmentsForCase_ClinicalChemistryResultsCheckBox);

            return this;
        }

        public PersonDocumentViewSubPage ClickSelectAllAttachmentsForPersonCheckBox()
        {
            WaitForElement(attachments_SelectAllAttachmentsForPersonCheckBox);
            Click(attachments_SelectAllAttachmentsForPersonCheckBox);

            return this;
        }
        public PersonDocumentViewSubPage ClickAttachmentsForPerson_AllAttachedDocumentsCheckBox()
        {
            WaitForElement(attachments_AttachmentsForPerson_AllAttachedDocumentsCheckBox);
            Click(attachments_AttachmentsForPerson_AllAttachedDocumentsCheckBox);

            return this;
        }
        public PersonDocumentViewSubPage ClickAttachmentsForPerson_ClinicalChemistryResultsCheckBox()
        {
            WaitForElement(attachments_AttachmentsForPerson_ClinicalChemistryResultsCheckBox);
            Click(attachments_AttachmentsForPerson_ClinicalChemistryResultsCheckBox);

            return this;
        }



        public PersonDocumentViewSubPage ClickAttachmentsExpandButton()
        {
            WaitForElement(attachmentsExpandButton);
            Click(attachmentsExpandButton);

            return this;
        }

        public PersonDocumentViewSubPage ClickAttachmentsForCaseExpandButton()
        {
            WaitForElement(attachmentsForCaseExpandButton);
            Click(attachmentsForCaseExpandButton);

            return this;
        }
        public PersonDocumentViewSubPage ClickAttachmentsForCase_AllAttachedDocumentsExpandButton()
        {
            WaitForElement(attachmentsForCase_AllAttachedDocumentsExpandButton);
            Click(attachmentsForCase_AllAttachedDocumentsExpandButton);

            return this;
        }
        public PersonDocumentViewSubPage ClickAttachmentsForCase_ClinicalChemistryResultsExpandButton()
        {
            WaitForElement(attachmentsForCase_ClinicalChemistryResultsExpandButton);
            Click(attachmentsForCase_ClinicalChemistryResultsExpandButton);

            return this;
        }

        public PersonDocumentViewSubPage ClickAttachmentsForPersonExpandButton()
        {
            WaitForElement(attachmentsForPersonExpandButton);
            Click(attachmentsForPersonExpandButton);

            return this;
        }
        public PersonDocumentViewSubPage ClickattAchmentsForPerson_AllAttachedDocumentsExpandButton()
        {
            WaitForElement(attachmentsForPerson_AllAttachedDocumentsExpandButton);
            Click(attachmentsForPerson_AllAttachedDocumentsExpandButton);

            return this;
        }
        public PersonDocumentViewSubPage ClickAttachmentsForPerson_ClinicalChemistryResultsExpandButton()
        {
            WaitForElement(attachmentsForPerson_ClinicalChemistryResultsExpandButton);
            Click(attachmentsForPerson_ClinicalChemistryResultsExpandButton);

            return this;
        }



        public PersonDocumentViewSubPage ClickAttachmentRecordLink(string DocumentID)
        {
            WaitForElementToBeClickable(attachmentLink(DocumentID));
            Click(attachmentLink(DocumentID));

            return this;
        }

        public PersonDocumentViewSubPage ClickAttachmentDownloadIcon(string DocumentID)
        {
            WaitForElementToBeClickable(attachmentDownloadIcon(DocumentID));
            Click(attachmentDownloadIcon(DocumentID));

            return this;
        }

        public PersonDocumentViewSubPage SelectAttachmentDocument(string DocumentID)
        {
            WaitForElement(attachmentDocumentCheckBox(DocumentID));
            Click(attachmentDocumentCheckBox(DocumentID));

            return this;
        }

        #endregion

        #region Letters

        public PersonDocumentViewSubPage ValidateLettersNoRecordsPresentMessageVisible()
        {
            WaitForElementVisible(letters_NoRecordsPresentMessage);

            return this;
        }

        public PersonDocumentViewSubPage ValidateLetterLinkText(string DocumentID, string ExpectedText)
        {
            ValidateElementText(letterLink(DocumentID), ExpectedText);

            return this;
        }

        public PersonDocumentViewSubPage ValidateLetterRecordVisible(string DocumentID, string FileId)
        {
            WaitForElementVisible(letterDocumentCheckBox(DocumentID));
            WaitForElementVisible(letterDownloadIcon(FileId));
            WaitForElementVisible(letterLink(DocumentID));

            return this;
        }

        public PersonDocumentViewSubPage ValidateLetterRecordNotVisible(string DocumentID, string FileId)
        {
            WaitForElementNotVisible(letterDocumentCheckBox(DocumentID), 3);
            WaitForElementNotVisible(letterDownloadIcon(FileId), 3);
            WaitForElementNotVisible(letterLink(DocumentID), 3);

            return this;
        }



        public PersonDocumentViewSubPage ClickSelectAllLettersCheckBox()
        {
            WaitForElement(letters_SelectAllCheckBox);
            Click(letters_SelectAllCheckBox);

            return this;
        }




        public PersonDocumentViewSubPage ClickLettersExpandButton()
        {
            WaitForElement(lettersExpandButton);
            Click(lettersExpandButton);

            return this;
        }



        public PersonDocumentViewSubPage ClickLetterRecordLink(string DocumentID)
        {
            WaitForElementToBeClickable(letterLink(DocumentID));
            Click(letterLink(DocumentID));

            return this;
        }

        public PersonDocumentViewSubPage ClickLetterDownloadIcon(string DocumentID)
        {
            WaitForElementToBeClickable(letterDownloadIcon(DocumentID));
            Click(letterDownloadIcon(DocumentID));

            return this;
        }

        public PersonDocumentViewSubPage SelectLetterDocument(string DocumentID)
        {
            WaitForElement(letterDocumentCheckBox(DocumentID));
            Click(letterDocumentCheckBox(DocumentID));

            return this;
        }

        #endregion

        public PersonDocumentViewSubPage ValidateCaseFormAttachedDocumentPosition(int Position, string RecordID)
        {
            WaitForElementVisible(CaseFormAttachmentRecordLinkPosition(Position, RecordID));

            return this;
        }

        public PersonDocumentViewSubPage ValidatePersonFormAttachedDocumentPosition(int Position, string RecordID)
        {
            WaitForElementVisible(PersonFormAttachmentRecordLinkPosition(Position, RecordID));

            return this;
        }

        public PersonDocumentViewSubPage ValidatePerson_CaseFormRecordPosition(int Position, string RecordID)
        {
            WaitForElementVisible(Person_Case_FormRecordLinkPosition(Position, RecordID));

            return this;
        }

        #endregion




    }
}
