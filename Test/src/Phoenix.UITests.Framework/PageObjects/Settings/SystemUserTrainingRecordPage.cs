
using Microsoft.Office.Interop.Word;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserTrainingRecordPage : CommonMethods
    {
        public SystemUserTrainingRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemusertraining&')]");
        
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back'][@onclick='CW.DataForm.Close(); return false;']");
        readonly By delete_Button = By.Id("TI_DeleteRecordButton");
        readonly By assignRecord_Button = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElements_Button = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");

        readonly By menuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By auditLinkButton = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");

        #region Field Labels

        readonly By Regarding_FieldLabel = By.XPath("//*[@id='CWLabelHolder_regardingid']/label");
        readonly By TrainingItem_FieldLabel = By.XPath("//*[@id='CWLabelHolder_trainingitemid']/label");
        readonly By TrainingCourse_FieldLabel = By.XPath("//*[@id='CWLabelHolder_trainingrequirementid']/label");
        readonly By TrainingCourseStartDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By TrainingCourseFinishDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_finishdate']/label");
        readonly By Outcome_FieldLabel = By.XPath("//*[@id='CWLabelHolder_outcomeid']/label");
        readonly By ExpiryDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_expirydate']/label");

        readonly By Status_FieldLabel = By.XPath("//*[@id='CWLabelHolder_stafftrainingstatusid']/label");
        readonly By ReferenceNumber_FieldLabel = By.XPath("//*[@id='CWLabelHolder_referencenumber']/label");
        readonly By Notes_FieldLabel = By.XPath("//*[@id='CWLabelHolder_notes']/label");
        readonly By ResponsibleTeam_FieldLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By TrainingAttachments_AreaLabel = By.XPath("//*[@id='CWSection_SystemUserTrainingAttachments']/fieldset/div/span");
        

        #endregion

        #region Fields

        readonly By Regarding_LinkField = By.XPath("//*[@id='CWField_regardingid_Link']");
        readonly By Regarding_LookupButton = By.XPath("//*[@id='CWLookupBtn_regardingid']");
        readonly By Regarding_RemoveButton = By.XPath("//*[@id='CWClearLookup_regardingid']");

        readonly By TrainingItem_LinkField = By.XPath("//*[@id='CWField_trainingitemid_Link']");
        readonly By TrainingItem_LookupButton = By.XPath("//*[@id='CWLookupBtn_trainingitemid']");
        readonly By TrainingItem_RemoveButton = By.XPath("//*[@id='CWClearLookup_trainingitemid']");
        readonly By TrainingItem_ErrorLabel = By.XPath("//*[@id='CWControlHolder_trainingitemid']/label/span");
        

        readonly By TrainingCourse_LinkField = By.XPath("//*[@id='CWField_trainingrequirementid_Link']");
        readonly By TrainingCourse_LookupButton = By.XPath("//*[@id='CWLookupBtn_trainingrequirementid']");
        readonly By TrainingCourse_RemoveButton = By.XPath("//*[@id='CWClearLookup_trainingrequirementid']");
        readonly By TrainingCourse_MandatoryField = By.XPath("//*[@id='CWLabelHolder_trainingrequirementid']/label/span[@class ='mandatory']");
        readonly By TrainingCourse_FieldErrorMessage = By.XPath("//*[@id ='CWControlHolder_trainingrequirementid']//span");

        readonly By TrainingCourseStartDate_Field = By.XPath("//*[@id='CWField_startdate']");
        
        readonly By TrainingCourseFinishDate_Field = By.XPath("//*[@id='CWField_finishdate']");
        
        readonly By Outcome_Picklist = By.XPath("//*[@id='CWField_outcomeid']");
        readonly By Outcome_MandatoryField = By.XPath("//*[@id='CWLabelHolder_outcomeid']/label/span[@class ='mandatory']");
        readonly By Outcome_FieldErrorMessage = By.XPath("//*[@id ='CWControlHolder_outcomeid']//span");

        readonly By ExpiryDate_Field = By.XPath("//*[@id='CWField_expirydate']");

        readonly By Status_Picklist = By.XPath("//*[@id='CWField_stafftrainingstatusid']");

        readonly By ReferenceNumber_Field = By.XPath("//*[@id='CWField_referencenumber']");

        readonly By Notes_Field = By.XPath("//*[@id='CWField_notes']");

        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleTeam_RemoveButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By ResponsibleTeam_ErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");

        readonly By TrainingAttachments_Area = By.XPath("//*[@id='CWExternalPage_SystemUserTrainingAttachmentItems']");

        #endregion





        public SystemUserTrainingRecordPage WaitForSystemUserTrainingRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(pageHeader);

            WaitForElementVisible(Regarding_FieldLabel);
            WaitForElementVisible(TrainingItem_FieldLabel);
            WaitForElementVisible(TrainingCourse_FieldLabel);
            WaitForElementVisible(TrainingCourseStartDate_FieldLabel);
            WaitForElementVisible(TrainingCourseFinishDate_FieldLabel);
            WaitForElementVisible(Outcome_FieldLabel);
            WaitForElementVisible(ExpiryDate_FieldLabel);

            WaitForElementVisible(Status_FieldLabel);
            WaitForElementVisible(ReferenceNumber_FieldLabel);
            WaitForElementVisible(Notes_FieldLabel);
            WaitForElementVisible(ResponsibleTeam_FieldLabel);

            return this;
        }

        public SystemUserTrainingRecordPage WaitForSystemUserTrainingRecordPageToLoadFromAdvancedSearch()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(pageHeader);

            WaitForElementVisible(Regarding_FieldLabel);
            WaitForElementVisible(TrainingItem_FieldLabel);
            WaitForElementVisible(TrainingCourse_FieldLabel);
            WaitForElementVisible(TrainingCourseStartDate_FieldLabel);
            WaitForElementVisible(TrainingCourseFinishDate_FieldLabel);
            WaitForElementVisible(Outcome_FieldLabel);
            WaitForElementVisible(ExpiryDate_FieldLabel);

            WaitForElementVisible(Status_FieldLabel);
            WaitForElementVisible(ReferenceNumber_FieldLabel);
            WaitForElementVisible(Notes_FieldLabel);
            WaitForElementVisible(ResponsibleTeam_FieldLabel);

            return this;
        }


        public SystemUserTrainingRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(back_Button);
            Click(back_Button);
            return this;
        }

        public SystemUserTrainingRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(delete_Button);
            Click(delete_Button);
            return this;
        }

        public SystemUserTrainingRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(save_Button);
            Click(save_Button);
            return this;
        }

        public SystemUserTrainingRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(save_Button);
            WaitForElementVisible(saveAndClose_Button);
            WaitForElementVisible(assignRecord_Button);
            WaitForElementVisible(delete_Button);            
            WaitForElementVisible(additionalToolbarElements_Button);

            return this;
        }

        public SystemUserTrainingRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndClose_Button);
            Click(saveAndClose_Button);
            return this;
        }



        public SystemUserTrainingRecordPage ClickRegardingLookupButton()
        {
            WaitForElementToBeClickable(Regarding_LookupButton);
            Click(Regarding_LookupButton);
            return this;
        }
        public SystemUserTrainingRecordPage ClickRegardingRemoveButton()
        {
            WaitForElementToBeClickable(Regarding_RemoveButton);
            Click(Regarding_RemoveButton);
            return this;
        }
        public SystemUserTrainingRecordPage ClickTrainingItemLookupButton()
        {
            WaitForElementToBeClickable(TrainingItem_LookupButton);
            Click(TrainingItem_LookupButton);
            return this;
        }
        public SystemUserTrainingRecordPage ClickTrainingItemRemoveButton()
        {
            WaitForElementToBeClickable(TrainingItem_RemoveButton);
            Click(TrainingItem_RemoveButton);
            return this;
        }
        public SystemUserTrainingRecordPage ClickTrainingCourseLookupButton()
        {
            WaitForElementToBeClickable(TrainingCourse_LookupButton);
            Click(TrainingCourse_LookupButton);
            return this;
        }
        public SystemUserTrainingRecordPage ClickTrainingCourseRemoveButton()
        {
            WaitForElementToBeClickable(TrainingCourse_RemoveButton);
            Click(TrainingCourse_RemoveButton);
            return this;
        }
        public SystemUserTrainingRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
            Click(ResponsibleTeam_LookupButton);
            return this;
        }
        public SystemUserTrainingRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButton);
            Click(ResponsibleTeam_RemoveButton);
            return this;
        }


        public SystemUserTrainingRecordPage InsertTrainingCourseStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(TrainingCourseStartDate_Field);
            SendKeys(TrainingCourseStartDate_Field, TextToInsert);
            SendKeysWithoutClearing(TrainingCourseStartDate_Field, Keys.Tab);
            return this;
        }
        public SystemUserTrainingRecordPage InsertTrainingCourseFinishDate(string TextToInsert)
        {
            WaitForElementToBeClickable(TrainingCourseFinishDate_Field);
            SendKeys(TrainingCourseFinishDate_Field, TextToInsert);
            SendKeysWithoutClearing(TrainingCourseFinishDate_Field, Keys.Tab);

            return this;
        }
        public SystemUserTrainingRecordPage InsertExpiryDate(string TextToInsert)
        {
            WaitForElementToBeClickable(ExpiryDate_Field);
            SendKeys(ExpiryDate_Field, TextToInsert);
            return this;
        }
        public SystemUserTrainingRecordPage InsertReferenceNumber(string TextToInsert)
        {
            WaitForElementToBeClickable(ReferenceNumber_Field);
            SendKeys(ReferenceNumber_Field, TextToInsert);
            return this;
        }
        public SystemUserTrainingRecordPage InsertNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Notes_Field);
            SendKeys(Notes_Field, TextToInsert);
            return this;
        }


        public SystemUserTrainingRecordPage SelectOutcome(string TextToSelect)
        {
            ScrollToElement(Outcome_Picklist);
            WaitForElementToBeClickable(Outcome_Picklist);
            SelectPicklistElementByText(Outcome_Picklist, TextToSelect);

            return this;
        }

        public SystemUserTrainingRecordPage ValidateOutcomeFieldDisplayed()
        {
            ScrollToElement(Outcome_Picklist);
            WaitForElementVisible(Outcome_Picklist);
            Assert.IsTrue(GetElementVisibility(Outcome_Picklist));

            return this;
        }


        public SystemUserTrainingRecordPage ValidateRegardingLinkFieldText(string ExpectedText)
        {
            WaitForElement(Regarding_LinkField);
            ScrollToElement(Regarding_LinkField);
            ValidateElementText(Regarding_LinkField, ExpectedText);
            return this;
        }
        public SystemUserTrainingRecordPage ValidateTrainingItemLinkFieldText(string ExpectedText)
        {
            WaitForElement(TrainingItem_LinkField);
            ValidateElementText(TrainingItem_LinkField, ExpectedText);
            return this;
        }
        public SystemUserTrainingRecordPage ValidateTrainingCourseLinkFieldText(string ExpectedText)
        {
            WaitForElement(TrainingCourse_LinkField);
            ValidateElementText(TrainingCourse_LinkField, ExpectedText);
            return this;
        }
        public SystemUserTrainingRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            WaitForElement(ResponsibleTeam_LinkField);
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);
            return this;
        }


        public SystemUserTrainingRecordPage ValidateTrainingCourseStartDateFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(TrainingCourseStartDate_Field);
            ValidateElementValue(TrainingCourseStartDate_Field, ExpectedValue);
            return this;
        }
        public SystemUserTrainingRecordPage ValidateTrainingCourseFinishDateFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(TrainingCourseFinishDate_Field);
            ValidateElementValue(TrainingCourseFinishDate_Field, ExpectedValue);
            return this;
        }
        public SystemUserTrainingRecordPage ValidateExpiryDateFieldValue(string ExpectedValue)
        {
            ScrollToElement(ExpiryDate_Field);
            WaitForElementVisible(ExpiryDate_Field);
            ValidateElementValue(ExpiryDate_Field, ExpectedValue);
            return this;
        }
        public SystemUserTrainingRecordPage ValidateReferenceNumberFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(ReferenceNumber_Field);
            ValidateElementValue(ReferenceNumber_Field, ExpectedValue);
            return this;
        }
        public SystemUserTrainingRecordPage ValidateNotesFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(Notes_Field);
            ValidateElementValue(Notes_Field, ExpectedValue);
            return this;
        }


        public SystemUserTrainingRecordPage ValidateOutcomeFieldSelectedText(string ExpectedText)
        {
            WaitForElementVisible(Outcome_Picklist);
            ValidatePicklistSelectedText(Outcome_Picklist, ExpectedText);
            return this;
        }
        public SystemUserTrainingRecordPage ValidateStatusFieldSelectedText(string ExpectedText)
        {
            WaitForElementVisible(Status_Picklist);
            ValidatePicklistSelectedText(Status_Picklist, ExpectedText);
            return this;
        }



        public SystemUserTrainingRecordPage ValidateTrainingCourseFinishDateFieldDisabled(bool ExpectedDisabled)
        {
            if(ExpectedDisabled)
            {
                ValidateElementDisabled(TrainingCourseFinishDate_Field);
            }
            else
            {
                ValidateElementNotDisabled(TrainingCourseFinishDate_Field);
            }

            
            return this;
        }
        public SystemUserTrainingRecordPage ValidateOutcomeFieldDisabled(bool ExpectedDisabled)
        {
            
            ScrollToElement(Outcome_Picklist);
            WaitForElementVisible(Outcome_Picklist);

            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Outcome_Picklist);
            }
            else
            {
                ValidateElementNotDisabled(Outcome_Picklist);
            }


            return this;
        }
        public SystemUserTrainingRecordPage ValidateExpiryDateFieldDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(ExpiryDate_Field);
            }
            else
            {
                ValidateElementNotDisabled(ExpiryDate_Field);
            }


            return this;
        }
        public SystemUserTrainingRecordPage ValidateStatusFieldDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Status_Picklist);
            }
            else
            {
                ValidateElementNotDisabled(Status_Picklist);
            }


            return this;
        }



        public SystemUserTrainingRecordPage ValidateTrainingItemErrorLableVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(TrainingItem_ErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(TrainingItem_ErrorLabel, 3);
            }


            return this;
        }
        public SystemUserTrainingRecordPage ValidateResponsibleTeamErrorLableVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(ResponsibleTeam_ErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ResponsibleTeam_ErrorLabel, 3);
            }


            return this;
        }
        public SystemUserTrainingRecordPage ValidateTrainingItemErrorLableText(string ExpectedText)
        {
            WaitForElementVisible(TrainingItem_ErrorLabel);
            ValidateElementText(TrainingItem_ErrorLabel, ExpectedText);
            return this;
        }
        public SystemUserTrainingRecordPage ValidateResponsibleTeamErrorLableText(string ExpectedText)
        {
            WaitForElementVisible(ResponsibleTeam_ErrorLabel);
            ValidateElementText(ResponsibleTeam_ErrorLabel, ExpectedText);
            return this;
        }
        public SystemUserTrainingRecordPage ValidateTrainingAttachmentsAreaVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(TrainingAttachments_AreaLabel);
                WaitForElementVisible(TrainingAttachments_Area);
            }
            else
            {
                WaitForElementNotVisible(TrainingAttachments_AreaLabel, 3);
                WaitForElementNotVisible(TrainingAttachments_Area, 3);
            }


            return this;
        }


        public SystemUserTrainingRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(menuButton);
            Click(menuButton);

            WaitForElementToBeClickable(auditLinkButton);
            Click(auditLinkButton);


            return this;
        }

        public SystemUserTrainingRecordPage ValidateTrainingCourseMandatoryField(bool ExpectVisible)
        {
            WaitForElement(TrainingCourse_FieldLabel);
            Assert.AreEqual(ExpectVisible, GetElementVisibility(TrainingCourse_MandatoryField));

            return this;
        }

        public SystemUserTrainingRecordPage ValidateOutcomeMandatoryField(bool ExpectVisible)
        {
            WaitForElement(Outcome_MandatoryField);
            Assert.AreEqual(ExpectVisible, GetElementVisibility(Outcome_MandatoryField));

            return this;
        }

        public SystemUserTrainingRecordPage ValidateNotificationMessageText(string ExpectedText)
        {
            WaitForElementVisible(NotificationMessage);
            ScrollToElement(NotificationMessage);
            Assert.AreEqual(ExpectedText, GetElementText(NotificationMessage));

            return this;
        }

        public SystemUserTrainingRecordPage ValidateTrainingCourseFieldErrorNotificationMessageText(string ExpectedText)
        {
            WaitForElementVisible(TrainingCourse_FieldErrorMessage);
            ScrollToElement(TrainingCourse_FieldErrorMessage);
            ValidateElementByTitle(TrainingCourse_FieldErrorMessage, ExpectedText);

            return this;
        }

        public SystemUserTrainingRecordPage ValidateOutcomeFieldErrorNotificationMessageText(string ExpectedText)
        {
            WaitForElementVisible(Outcome_FieldErrorMessage);
            ScrollToElement(Outcome_FieldErrorMessage);
            ValidateElementByTitle(Outcome_FieldErrorMessage, ExpectedText);

            return this;
        }

    }
}
