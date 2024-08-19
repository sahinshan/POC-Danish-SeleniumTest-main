using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ConsultantEpisodesRecordPage : CommonMethods
    {
        public ConsultantEpisodesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By inpatientConsultantEpisodeFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=inpatientconsultantepisode&')]");

        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By RunOnDemandWorkflow = By.Id("TI_RunOnDemandWorkflow");
        readonly By CopyRecordLink = By.Id("TI_CopyRecordLink");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By CWNavItem_InpatientLeaveAwolCaseNote = By.XPath("//*[@id='CWNavItem_InpatientLeaveAwolCaseNote']");




        #region Field Labels

        readonly By personFieldLabel = By.XPath("//li[@id='CWLabelHolder_personid']/label[text()='Person']/Span[text()='*']");

        #endregion

        #region Fields

        readonly By personField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_personid_Link']");
        readonly By personLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_personid']");


        readonly By relatedCaseId_Field_LookUpButton = By.Id("CWLookupBtn_caseid");
        readonly By consultantName_Field_LookUpButton = By.Id("CWLookupBtn_consultantid");
        readonly By startDate_Field = By.Id("CWField_startdatetime");
        readonly By startTime_Field = By.Id("CWField_startdatetime_Time");
        readonly By endDate_Field = By.Id("CWField_enddatetime");
        readonly By treatmentFunctionCode_Field_LookUpButton = By.Id("CWLookupBtn_inpatienttreatmentfunctioncodeid");
        readonly By endReason_Field_LookUpButton = By.Id("CWLookupBtn_inpatientconsultantepisodeendreasonid");
        readonly By description_Field = By.Id("CWField_description");


        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By consultantName_Field_ErrorMessage = By.XPath("//*[@id='CWControlHolder_consultantid']/label/span");
        readonly By startDate_Field_ErrorMessage = By.XPath("//*[@id='CWControlHolder_startdatetime']/div/div[1]/label/span");
        readonly By startTime_Field_ErrorMessage = By.XPath("//*[@id='CWControlHolder_startdatetime']/div/div[2]/label/span");

        readonly By RTTTreatmentStatus_LookupButton = By.Id("CWLookupBtn_rtttreatmentstatusid");

        #endregion


        public ConsultantEpisodesRecordPage WaitForConsultantEpisodesRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(inpatientConsultantEpisodeFrame);
            SwitchToIframe(inpatientConsultantEpisodeFrame);

            WaitForElement(pageHeader);

            WaitForElement(personFieldLabel);

            return this;
        }

        public ConsultantEpisodesRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(backButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

        public ConsultantEpisodesRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public ConsultantEpisodesRecordPage TapSaveButton()
        {
            driver.FindElement(saveButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public ConsultantEpisodesRecordPage TapSaveAndCloseButton()
        {
            driver.FindElement(saveAndCloseButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public ConsultantEpisodesRecordPage ClickRelatedCaseIdLookupButton()
        {
            WaitForElement(relatedCaseId_Field_LookUpButton);
            Click(relatedCaseId_Field_LookUpButton);

            return this;
        }

        public ConsultantEpisodesRecordPage ClickEndReasonLookupButton()
        {
            WaitForElement(endReason_Field_LookUpButton);
            Click(endReason_Field_LookUpButton);

            return this;
        }

        public ConsultantEpisodesRecordPage ClickConsultantNameLookupButton()
        {
            WaitForElement(consultantName_Field_LookUpButton);
            Click(consultantName_Field_LookUpButton);

            return this;
        }

        public ConsultantEpisodesRecordPage InsertStartDateText(string TextToInsert)
        {
            WaitForElement(startDate_Field);
            SendKeys(startDate_Field, TextToInsert);

            return this;
        }

        public ConsultantEpisodesRecordPage InsertStartDateTimeText(string TextToInsert)
        {
            WaitForElement(startTime_Field);
            SendKeys(startTime_Field, TextToInsert);

            return this;
        }

        public ConsultantEpisodesRecordPage InsertEndDateText(string TextToInsert)
        {
            WaitForElement(endDate_Field);
            SendKeys(endDate_Field, TextToInsert);

            return this;
        }

        public ConsultantEpisodesRecordPage ClickTreatmentFunctionCode()
        {
            WaitForElement(treatmentFunctionCode_Field_LookUpButton);
            Click(treatmentFunctionCode_Field_LookUpButton);

            return this;
        }

        public ConsultantEpisodesRecordPage InsertDescriptionText(string TextToInsert)
        {
            WaitForElement(description_Field);
            SendKeys(description_Field, TextToInsert);

            return this;
        }

        public ConsultantEpisodesRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(notificationMessage);
            }
            else
            {
                WaitForElementNotVisible(notificationMessage, 3);
            }

            return this;
        }
        public ConsultantEpisodesRecordPage ValidateMessageAreaText(String ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public ConsultantEpisodesRecordPage ValidateConsultantNameArea(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(consultantName_Field_ErrorMessage);
            }
            else
            {
                WaitForElementNotVisible(consultantName_Field_ErrorMessage, 3);
            }

            return this;
        }

        public ConsultantEpisodesRecordPage ValidateConsultantNameAreaText(String ExpectedText)
        {
            ValidateElementText(consultantName_Field_ErrorMessage, ExpectedText);

            return this;
        }

        public ConsultantEpisodesRecordPage ValidateStartDateFieldArea(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(startDate_Field_ErrorMessage);
            }
            else
            {
                WaitForElementNotVisible(startDate_Field_ErrorMessage, 3);
            }

            return this;
        }

        public ConsultantEpisodesRecordPage ValidateStartDateFieldText(String ExpectedText)
        {
            ValidateElementText(startDate_Field_ErrorMessage, ExpectedText);

            return this;
        }


        public ConsultantEpisodesRecordPage ValidateStartTimeFieldArea(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(startTime_Field_ErrorMessage);
            }
            else
            {
                WaitForElementNotVisible(startTime_Field_ErrorMessage, 3);
            }

            return this;
        }

        public ConsultantEpisodesRecordPage ValidateStartTimeFieldText(String ExpectedText)
        {
            ValidateElementText(startTime_Field_ErrorMessage, ExpectedText);

            return this;
        }

        public ConsultantEpisodesRecordPage ClickRTTTreatmentStatusLookupButton()
        {
            WaitForElementToBeClickable(RTTTreatmentStatus_LookupButton);
            MoveToElementInPage(RTTTreatmentStatus_LookupButton);
            Click(RTTTreatmentStatus_LookupButton);

            return this;
        }

    }
}
