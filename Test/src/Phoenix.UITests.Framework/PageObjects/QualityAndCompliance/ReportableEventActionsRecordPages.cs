using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventActionsRecordPage : CommonMethods
    {
        public ReportableEventActionsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContent_Iframe = By.XPath("//iframe[@id='CWContentIFrame']");
        readonly By careproviderReportableEventActions_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableeventaction&')]");

        readonly By responsibleUses_Link = By.Id("CWField_responsibleuserid_Link");
        readonly By responsibleTeam_Link = By.Id("CWField_ownerid_Link");
        readonly By reportableEvent_Link = By.Id("CWField_careproviderreportableeventid_Link");


        readonly By action_TextBox = By.Id("CWField_action");
        readonly By decision_TextBox = By.Id("CWField_decision");

        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By nextReviewDate_Field = By.Id("CWField_nextreviewdate");
        readonly By endDate_Field = By.Id("CWField_enddate");
        readonly By dueDate_Field = By.Id("CWField_duedate");
        readonly By status_Field = By.Id("CWField_careproviderreportableeventactionstatusid");
        readonly By actionId_Field = By.XPath("//*[@id='CWField_identifier']");
        readonly By action_Field = By.Id("CWField_action");

        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");

        #region Label

        readonly By actionId_MandantoryField = By.XPath("//*[@id='CWLabelHolder_identifier']/label/span");
        readonly By reportableEventId_MandantoryField = By.XPath("//*[@id='CWLabelHolder_careproviderreportableeventid']/label/span");
        readonly By responsibleUser_MandantoryField = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label/span");
        readonly By responsibleTeam_MandantoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span");
        readonly By action_MandantoryField = By.XPath("//*[@id='CWLabelHolder_action']/label/span");
        readonly By statusId_MandantoryField = By.XPath("//*[@id='CWLabelHolder_careproviderreportableeventactionstatusid']/label/span");
        readonly By decision_MandantoryField = By.XPath("//*[@id='CWLabelHolder_decision']/label/span");
        readonly By startDate_MandantoryField = By.XPath("//*[@id='CWLabelHolder_startdate']/label/span");
        readonly By endDate_MandantoryField = By.XPath("//*[@id='CWLabelHolder_enddate']/label/span");
        readonly By nextReviewDate_MandantoryField = By.XPath("//*[@id='CWLabelHolder_enddate']/label/span");
        readonly By dueDate_MandantoryField = By.XPath("//*[@id='CWLabelHolder_duedate']/label/span");

        #endregion

        #region Lookup

        readonly By responsibleUser_Lookup = By.Id("CWLookupBtn_responsibleuserid");
        readonly By responsibleTeam_Lookup = By.Id("CWLookupBtn_ownerid");
        readonly By assignedUser_Lookup = By.Id("CWLookupBtn_assigneduserid");

        #endregion



        public ReportableEventActionsRecordPage WaitForReportableEventActionsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElement(careproviderReportableEventActions_Iframe);
            SwitchToIframe(careproviderReportableEventActions_Iframe);

            return this;
        }
        public ReportableEventActionsRecordPage ValidateActionIdMandatoryFieldAndNonEditable(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(actionId_MandantoryField);
            }
            else
            {
                WaitForElementNotVisible(actionId_MandantoryField, 3);
            }

            WaitForElement(actionId_Field);
            ValidateElementDisabled(actionId_Field);

            return this;
        }
        public ReportableEventActionsRecordPage ValidateRelatedReportableEventMandatoryFieldAndPreFilled(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(reportableEventId_MandantoryField);
            }
            else
            {
                WaitForElementNotVisible(reportableEventId_MandantoryField, 3);
            }

            return this;
        }

        public ReportableEventActionsRecordPage ValidateResponsibleUserMandatoryFieldAndPreFilled(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(responsibleUser_MandantoryField);
            }
            else
            {
                WaitForElementNotVisible(responsibleUser_MandantoryField, 3);
            }

            return this;
        }

        public ReportableEventActionsRecordPage ValidateResponsibleTeamMandatoryFieldAndPreFilled(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(responsibleTeam_MandantoryField);
            }
            else
            {
                WaitForElementNotVisible(responsibleTeam_MandantoryField, 3);
            }

            return this;
        }

        public ReportableEventActionsRecordPage ValidateActionMandatoryField(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(action_MandantoryField);
            }
            else
            {
                WaitForElementNotVisible(action_MandantoryField, 3);
            }

            return this;
        }

        public ReportableEventActionsRecordPage ValidateStatusMandatoryField(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(statusId_MandantoryField);
            }
            else
            {
                WaitForElementNotVisible(statusId_MandantoryField, 3);
            }

            return this;
        }
        public ReportableEventActionsRecordPage ValidateDecisionNonMandatoryField(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(decision_MandantoryField);
            }
            else
            {
                WaitForElementNotVisible(decision_MandantoryField, 3);
            }

            return this;
        }
        public ReportableEventActionsRecordPage ValidateStartDateMandatoryField(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(startDate_MandantoryField);
            }
            else
            {
                WaitForElementNotVisible(startDate_MandantoryField, 3);
            }

            return this;
        }
        public ReportableEventActionsRecordPage ValidateEndDateNonMandatoryField(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(endDate_MandantoryField);
            }
            else
            {
                WaitForElementNotVisible(endDate_MandantoryField, 3);
            }

            return this;
        }
        public ReportableEventActionsRecordPage ValidateNextReviewDateNonMandatoryField(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(nextReviewDate_MandantoryField);
            }
            else
            {
                WaitForElementNotVisible(nextReviewDate_MandantoryField, 3);
            }

            return this;
        }
        public ReportableEventActionsRecordPage ValidateDueDateMandatoryNonField(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(dueDate_MandantoryField);
            }
            else
            {
                WaitForElementNotVisible(dueDate_MandantoryField, 3);
            }

            return this;
        }
        public ReportableEventActionsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndClose_Button);
            Click(saveAndClose_Button);

            return this;
        }
        public ReportableEventActionsRecordPage ClickResponsibleUserLookup()
        {
            WaitForElement(responsibleUser_Lookup);
            Click(responsibleUser_Lookup);

            return this;
        }
        public ReportableEventActionsRecordPage ClickResponsibleTeamLookup()
        {
            WaitForElement(responsibleTeam_Lookup);
            Click(responsibleTeam_Lookup);

            return this;
        }
        public ReportableEventActionsRecordPage ClickAssignedUserLookup()
        {
            WaitForElementToBeClickable(assignedUser_Lookup);
            Click(assignedUser_Lookup);

            return this;
        }
        public ReportableEventActionsRecordPage InsertTextActionField(string key)
        {
            WaitForElement(action_Field);
            SendKeys(action_Field, key);

            return this;
        }
        public ReportableEventActionsRecordPage InsertTextDecisionField(string key)
        {
            WaitForElement(decision_TextBox);
            SendKeys(decision_TextBox, key);

            return this;
        }
        public ReportableEventActionsRecordPage SelectStatusId(string ElementText)
        {
            WaitForElement(status_Field);
            SelectPicklistElementByText(status_Field, ElementText);

            return this;
        }
        public ReportableEventActionsRecordPage InsertStartDateField(string key)
        {
            WaitForElement(startDate_Field);
            SendKeys(startDate_Field, key);

            return this;
        }
        public ReportableEventActionsRecordPage InsertEndDateField(string key)
        {
            WaitForElement(endDate_Field);
            SendKeys(endDate_Field, key);

            return this;
        }
        public ReportableEventActionsRecordPage InsertNextReviewDateField(string key)
        {
            WaitForElement(nextReviewDate_Field);
            SendKeys(nextReviewDate_Field, key);

            return this;
        }
        public ReportableEventActionsRecordPage InsertDueDateField(string key)
        {
            WaitForElement(dueDate_Field);
            SendKeys(dueDate_Field, key);

            return this;
        }
    }
}
