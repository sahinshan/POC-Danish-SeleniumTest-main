
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ApplicantAliasRecordPage : CommonMethods
    {
        public ApplicantAliasRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region IFrame
        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=applicantalias&')]");

        #endregion IFrame



        #region Fields

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Applicant Alias: ']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By BackButton = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");


        readonly By applicant_Field = By.Id("CWField_applicantid");
        readonly By applicant_LinkField = By.Id("CWField_applicantid_Link");
        readonly By applicant_LookUpButton = By.Id("CWLookupBtn_applicantid");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

        readonly By aliasType_Field = By.Id("CWField_applicantaliastypeid_cwname");
        readonly By aliasType_LookUpButton = By.Id("CWLookupBtn_applicantaliastypeid");
        readonly By firstName_Field = By.Id("CWField_firstname");
        readonly By middleName_Field = By.Id("CWField_middlename");
        readonly By lastName_Field = By.Id("CWField_lastname");

        readonly By preferredName_YesRadioButton = By.Id("CWField_preferredname_1");
        readonly By preferredName_NoRadioButton = By.Id("CWField_preferredname_0");


        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By AuditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");


        #endregion

        #region MandatoryFields

        readonly By applicant_MandatoryField = By.XPath("//*[@id='CWLabelHolder_applicantid']/label/span[@class ='mandatory']");
        readonly By preferredName_MandatoryField = By.XPath("//*[@id='CWLabelHolder_preferredname']/label/span[@class ='mandatory']");
        readonly By lastName_MandatoryField = By.XPath("//*[@id='CWLabelHolder_lastname']/label/span[@class ='mandatory']");

        #endregion MandatoryFields


        #region ErrorMessages

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By lastName_ErrorLabel = By.XPath("//*[@id='CWControlHolder_lastname']/label/span");

        #endregion ErrorMessages


        public ApplicantAliasRecordPage WaitForApplicantAliasRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            return this;
        }

        public ApplicantAliasRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteButton);
            WaitForElementVisible(AdditionalItemsButton);

            return this;
        }

        public ApplicantAliasRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteButton);
            ScrollToElement(DeleteButton);
            Click(DeleteButton);
            return this;
        }

        public ApplicantAliasRecordPage ClickAdditionalItemsButton()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            ScrollToElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            return this;
        }

        public ApplicantAliasRecordPage ClickBackButton()
        {

            WaitForElementToBeClickable(BackButton);
            ScrollToElement(BackButton);
            Click(BackButton);

            return this;
        }


        public ApplicantAliasRecordPage ValidateApplicantAliasRecordPageTitle(string PageTitle)
        {

            WaitForElementVisible(pageHeader);
            ScrollToElement(pageHeader);
            ValidateElementTextContainsText(pageHeader, PageTitle);
            return this;
        }

        public ApplicantAliasRecordPage WaitForApplicantAliasRecordPageTitleToLoad(string PageTitle)
        {
            WaitForApplicantAliasRecordPageToLoad();

            WaitForElementToContainText(pageHeader, "ApplicantAlias:  " + PageTitle);

            return this;
        }


        public ApplicantAliasRecordPage ValidateApplicantAlias_LinkField(string ExpectedText)
        {
            WaitForElementVisible(applicant_LinkField);
            ScrollToElement (applicant_LinkField);
            ValidateElementTextContainsText(applicant_LinkField, ExpectedText);

            return this;
        }

        public ApplicantAliasRecordPage ValidateApplicantLookUpButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(applicant_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(applicant_LookUpButton);
            }

            return this;
        }

        public ApplicantAliasRecordPage ValidateResponsibleTeamLookUpButtonDisabled(bool expectedDisabled)
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

        public ApplicantAliasRecordPage ValidateAliasTypeLookUpButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(aliasType_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(aliasType_LookUpButton);
            }

            return this;
        }

        public ApplicantAliasRecordPage ValidateAliasType_Editable(bool ExpectedText)
        {
            if (ExpectedText)
            {

                WaitForElementVisible(aliasType_Field);
                ValidateElementEnabled(aliasType_Field);

            }
            else
            {
                WaitForElementNotVisible(aliasType_Field, 5);

            }
            return this;
        }

        public ApplicantAliasRecordPage ClickApplicantLookUpButton()
        {
            WaitForElementToBeClickable(applicant_LookUpButton);
            ScrollToElement(applicant_LookUpButton);
            Click(applicant_LookUpButton);

            return this;
        }

        public ApplicantAliasRecordPage ClickPreferredName_YesRadioButton()
        {
            WaitForElementToBeClickable(preferredName_YesRadioButton);
            ScrollToElement(preferredName_YesRadioButton);
            Click(preferredName_YesRadioButton);

            return this;
        }

        public ApplicantAliasRecordPage ClickPreferredName_NoRadioButton()
        {
            WaitForElementToBeClickable(preferredName_NoRadioButton);
            ScrollToElement(preferredName_NoRadioButton);
            Click(preferredName_NoRadioButton);

            return this;
        }


        public ApplicantAliasRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            ScrollToElement(saveButton);
            Click(saveButton);
            return this;
        }

        public ApplicantAliasRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            ScrollToElement(saveAndCloseButton);
            Click(saveAndCloseButton);
            return this;
        }

        public ApplicantAliasRecordPage ValidateAvailableFields()
        {

            ValidateElementEnabled(applicant_Field);
            ValidateElementEnabled(aliasType_Field);
            ValidateElementEnabled(firstName_Field);
            ValidateElementEnabled(middleName_Field);
            ValidateElementEnabled(lastName_Field);
            ValidateElementEnabled(preferredName_YesRadioButton);
            ValidateElementEnabled(preferredName_NoRadioButton);

            return this;
        }


        public ApplicantAliasRecordPage ValidateFirstNameFieldValue(string ExpectedText)
        {

            WaitForElementVisible(firstName_Field);
            ScrollToElement(firstName_Field);
            ValidateElementValue(firstName_Field, ExpectedText);

            return this;
        }

        public ApplicantAliasRecordPage ValidateMiddleNameFieldValue(string ExpectedText)
        {
            WaitForElementVisible(middleName_Field);
            ScrollToElement(middleName_Field);
            ValidateElementValue(middleName_Field, ExpectedText);
            return this;
        }

        public ApplicantAliasRecordPage ValidateLastNameFieldValue(string ExpectedText)
        {
            WaitForElementVisible(lastName_Field);
            ScrollToElement(lastName_Field);
            ValidateElementValue(lastName_Field, ExpectedText);
            return this;
        }


        public ApplicantAliasRecordPage ValidateSystemUserMandatoryField()
        {


            WaitForElementVisible(applicant_MandatoryField);
            Assert.IsTrue(GetElementVisibility(applicant_MandatoryField));

            return this;
        }

        public ApplicantAliasRecordPage ValidateLastNameMandatoryField()
        {


            WaitForElementVisible(lastName_MandatoryField);
            Assert.IsTrue(GetElementVisibility(lastName_MandatoryField));

            return this;
        }

        public ApplicantAliasRecordPage ValidatePreferredNameMandatoryField()
        {


            WaitForElementVisible(preferredName_MandatoryField);
            Assert.IsTrue(GetElementVisibility(preferredName_MandatoryField));

            return this;
        }


        public ApplicantAliasRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            WaitForElementVisible(notificationMessage);
            ScrollToElement(notificationMessage);
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public ApplicantAliasRecordPage ValidateLastNameFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(lastName_ErrorLabel);
            ScrollToElement(lastName_ErrorLabel);
            ValidateElementText(lastName_ErrorLabel, ExpectedText);

            return this;
        }

        public ApplicantAliasRecordPage InsertFirstName(string TextToInsert)
        {
            WaitForElementVisible(firstName_Field);
            ScrollToElement(firstName_Field);
            SendKeys(firstName_Field, TextToInsert);

            return this;
        }

        public ApplicantAliasRecordPage InsertMiddleName(string TextToInsert)
        {
            WaitForElementVisible(middleName_Field);
            ScrollToElement(middleName_Field);
            SendKeys(middleName_Field, TextToInsert);

            return this;
        }

        public ApplicantAliasRecordPage InsertLastName(string TextToInsert)
        {
            WaitForElementVisible(lastName_Field);
            ScrollToElement(lastName_Field);
            SendKeys(lastName_Field, TextToInsert);

            return this;
        }

        public ApplicantAliasRecordPage ClickAliasTypeLookUpButton()
        {
            WaitForElementToBeClickable(aliasType_LookUpButton);
            Click(aliasType_LookUpButton);

            return this;
        }

        public ApplicantAliasRecordPage NavigateToAuditSubPage()
        {
            WaitForElementToBeClickable(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementToBeClickable(AuditLink_LeftMenu);
            Click(AuditLink_LeftMenu);

            return this;
        }
    }
}
