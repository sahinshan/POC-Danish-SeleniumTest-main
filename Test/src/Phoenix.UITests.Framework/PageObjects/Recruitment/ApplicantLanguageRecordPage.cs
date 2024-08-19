using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class ApplicantLanguageRecordPage : CommonMethods
    {
        public ApplicantLanguageRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=applicantlanguage')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");


        readonly By saveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");


        #region Fields

        readonly By Applicant_FieldHeader = By.Id("CWLabelHolder_applicantid");
        readonly By Applicant_LinkField = By.XPath("//*[@id='CWField_applicantid_Link']");
        readonly By Applicant_LookUpButton = By.Id("CWLookupBtn_applicantid");        
        readonly By Applicant_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_applicantid']/label/span");

        readonly By Language_FieldHeader = By.Id("CWLabelHolder_languageid");
        readonly By Language_LinkField = By.XPath("//*[@id='CWField_languageid_Link']");
        readonly By Language_LookUpButton = By.Id("CWLookupBtn_languageid");
        readonly By Language_RemoveButton = By.Id("CWClearLookup_languageid");
        readonly By Language_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_languageid']/label/span");

        readonly By StartDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By StartDate_DateField = By.Id("CWField_startdate");
        readonly By StartDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_startdate']/label/span");

        readonly By Fluency_FieldHeader = By.XPath("//*[@id='CWLabelHolder_fluencyid']/label");
        readonly By Fluency_LinkField = By.XPath("//*[@id='CWField_fluencyid_Link']");
        readonly By Fluency_Field = By.Id("CWField_fluencyid");
        readonly By Fluency_LookUpButton = By.Id("CWLookupBtn_fluencyid");
        readonly By Fluency_RemoveButton = By.Id("CWClearLookup_fluencyid");

        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_Field = By.Id("CWField_ownerid");
        readonly By ResponsibleTeam_LinkField = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

        #endregion


        public ApplicantLanguageRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public ApplicantLanguageRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }

        public ApplicantLanguageRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteButton);
            WaitForElementVisible(AdditionalItemsButton);

            return this;
        }

        public ApplicantLanguageRecordPage ClickDeleteButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            WaitForElementVisible(DeleteButton);
            Click(DeleteButton);
            return this;
        }

        public ApplicantLanguageRecordPage ClickAdditionalItemsButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            return this;
        }

        public ApplicantLanguageRecordPage ClickBackButton()
        {

            WaitForElementToBeClickable(Back_Button);
            ScrollToElement(Back_Button);
            Click(Back_Button);

            return this;
        }

        public ApplicantLanguageRecordPage WaitForApplicantLanguageRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(Applicant_FieldHeader);
            WaitForElement(Language_FieldHeader);
            WaitForElement(StartDate_FieldHeader);
            WaitForElement(Fluency_FieldHeader);

            return this;
        }


        public ApplicantLanguageRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
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
        public ApplicantLanguageRecordPage ValidateApplicantFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Applicant_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Applicant_FieldErrorLabel, 3);
            }

            return this;
        }
        public ApplicantLanguageRecordPage ValidateLanguageFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Language_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Language_FieldErrorLabel, 3);
            }

            return this;
        }
        public ApplicantLanguageRecordPage ValidateStartDateFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(StartDate_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(StartDate_FieldErrorLabel, 3);
            }

            return this;
        }


        public ApplicantLanguageRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public ApplicantLanguageRecordPage ValidateLanguageFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Language_FieldErrorLabel, ExpectedText);

            return this;
        }

        public ApplicantLanguageRecordPage ValidateStartDateFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(StartDate_FieldErrorLabel, ExpectedText);

            return this;
        }

        public ApplicantLanguageRecordPage ValidateApplicantFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Applicant_FieldErrorLabel, ExpectedText);

            return this;
        }

        public ApplicantLanguageRecordPage InsertStartDate(string DateToInsert)
        {
            WaitForElement(StartDate_DateField);
            SendKeys(StartDate_DateField, DateToInsert);


            return this;
        }


        public ApplicantLanguageRecordPage ClickApplicantLookUpButton()
        {
            WaitForElementToBeClickable(Applicant_LookUpButton);
            Click(Applicant_LookUpButton);

            return this;
        }

        public ApplicantLanguageRecordPage ClickLanguageLookUpButton()
        {
            WaitForElementToBeClickable(Language_LookUpButton);
            Click(Language_LookUpButton);

            return this;
        }
        public ApplicantLanguageRecordPage ClickLanguageRemoveButton()
        {
            WaitForElementToBeClickable(Language_RemoveButton);
            Click(Language_RemoveButton);

            return this;
        }


        public ApplicantLanguageRecordPage ClickFluencyLookUpButton()
        {
            WaitForElementToBeClickable(Fluency_LookUpButton);
            Click(Fluency_LookUpButton);

            return this;
        }

        public ApplicantLanguageRecordPage ClickResponsibleTeamLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
            ScrollToElement(ResponsibleTeam_LookupButton);
            Click(ResponsibleTeam_LookupButton);
            return this;
        }

        public ApplicantLanguageRecordPage ClickFluencyRemoveButton()
        {
            WaitForElementToBeClickable(Fluency_RemoveButton);
            Click(Fluency_RemoveButton);

            return this;
        }

        public ApplicantLanguageRecordPage ValidatApplicantLinkFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(Applicant_LinkField);
            ScrollToElement(Applicant_LinkField);
            ValidateElementText(Applicant_LinkField, ExpectedText);

            return this;
        }
        public ApplicantLanguageRecordPage ValidateLanguageLinkFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(Language_LinkField);
            ScrollToElement(Language_LinkField);
            ValidateElementText(Language_LinkField, ExpectedText);

            return this;
        }
        public ApplicantLanguageRecordPage ValidateFluencyLinkFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(Fluency_LinkField);
            ScrollToElement(Fluency_LinkField);
            ValidateElementText(Fluency_LinkField, ExpectedText);

            return this;
        }


        public ApplicantLanguageRecordPage ValidateStartDate(string ExpectedDate)
        {
            WaitForElementVisible(StartDate_DateField);
            ScrollToElement(StartDate_DateField);
            ValidateElementValue(StartDate_DateField, ExpectedDate);

            return this;
        }

        public ApplicantLanguageRecordPage ValidateResponsibleTeamLinkFieldText(string expectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeam_LinkField);
            ScrollToElement(ResponsibleTeam_LinkField);
            ValidateElementByTitle(ResponsibleTeam_LinkField, expectedText);
            return this;
        }


        public ApplicantLanguageRecordPage ValidateApplicantLookUpButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Applicant_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(Applicant_LookUpButton);
            }

            return this;
        }

        public ApplicantLanguageRecordPage ValidateLanguageLookUpButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Language_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(Language_LookUpButton);
            }

            return this;
        }

        public ApplicantLanguageRecordPage ValidateStartDateFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(StartDate_DateField);
            }
            else
            {
                ValidateElementNotDisabled(StartDate_DateField);
            }

            return this;
        }

        public ApplicantLanguageRecordPage ValidateFluencyLookUpButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Fluency_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(Fluency_LookUpButton);
            }

            return this;
        }

        public ApplicantLanguageRecordPage ValidateResponsibleTeamLookUpButtonDisabled(bool expectedDisabled)
        {
            if (expectedDisabled)
            {
                ValidateElementDisabled(ResponsibleTeam_LookupButton);
            }
            else
            {
                ValidateElementNotDisabled(ResponsibleTeam_LookupButton);
            }
            return this;
        }

    }
}
