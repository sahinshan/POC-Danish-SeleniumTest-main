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
    public class OrganisationalRiskManagementRecordPage : CommonMethods
    {
        public OrganisationalRiskManagementRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By OrgRiskRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=organisationalrisk&')]");


        readonly By pageHeader = By.XPath("//h1[@title='Organisational Risk: New']");
        readonly By BackButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
       
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");



        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By attachments_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_InpatientSeclusionAttachment']");
        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

        #endregion

        #region Risk Details Fields

        readonly By RiskNumber_Field = By.Id("CWField_risknumber");
        readonly By RiskType_Field = By.Id("CWField_organisationalrisktypeid_cwname");
        readonly By RiskType_MandatoryField = By.XPath("//*[@id='CWLabelHolder_organisationalrisktypeid']/label/span[text()='*']");
        readonly By ResponsibleTeam_Field = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_LookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span");
        readonly By RiskType_ErrorLabel = By.XPath("//*[@id='CWControlHolder_organisationalrisktypeid']/label/span");
        readonly By RiskType_LookupButton = By.Id("CWLookupBtn_organisationalrisktypeid");
        readonly By RiskDescription_Field = By.Id("CWField_riskdescription");
        readonly By RiskStatus_Field = By.Id("CWField_riskstatusid");
        readonly By RiskStatus_MandatoryField = By.XPath("//*[@id='CWLabelHolder_riskstatusid']/label/span");
        readonly By CorporateRiskRegister_Field = By.Id("CWField_corporateriskregisterid");
        readonly By CorporateRiskRegister_MandatoryField = By.XPath("//*[@id='CWLabelHolder_corporateriskregisterid']/label/span");
        readonly By CorporateRiskRegister_ErrorLabel = By.XPath("//*[@id='CWControlHolder_corporateriskregisterid']/label/span");
        readonly By ResponsibleUser_Field = By.Id("CWField_responsibleuserid_cwname");
        readonly By ResponsibleUser_LookupButton = By.Id("CWLookupBtn_responsibleuserid");
        readonly By RiskIdentificationDate_Field = By.Id("CWField_riskidentificationdate");
        readonly By RiskIdentificationDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_riskidentificationdate']/label/span");
        readonly By NextReviewDate_Field = By.Id("CWField_nextreviewdate");
        readonly By RiskClosedDate_Field = By.Id("CWField_riskcloseddate");
        readonly By RiskClosedDate_AutopopulatedFieldValue = By.XPath("//*[@id='CWField_riskcloseddate' and @dirty = 'true']");

        
        #endregion

        #region Initial Risk Score Fields
        readonly By ConsequenceField_Label = By.XPath("//li[@id = 'CWLabelHolder_consequences']/label[text() = 'Consequence']");
        readonly By Consequence_Field = By.Id("CWField_consequences");
        readonly By Consequence_titleField = By.XPath("//*[@id='CWField_consequences' and @title = 'Min 1 - Max 5']");
        readonly By Consequence_ErrorLabel = By.XPath("//*[@id='CWControlHolder_consequences']/label/span");
        readonly By Likelihood_Field = By.Id("CWField_likelihood");
        readonly By Likelihood_titleField = By.XPath("//*[@id='CWField_likelihood' and @title = 'Min 1 - Max 5']");
        readonly By Likelihood_ErrorLabel = By.XPath("//*[@id='CWControlHolder_likelihood']/label/span");

        readonly By InitialRiskScore_Field = By.Id("CWField_initialriskscore");
        readonly By RiskCategory_Field = By.Id("CWField_initialriskcategory");
        #endregion

        #region Residual Risk Score Fields
        readonly By ResidualConsequenceField_Label = By.XPath("//li[@id = 'CWLabelHolder_residualconsequences']/label[text() = 'Residual Consequence']");
        readonly By ResidualConsequence_Field = By.Id("CWField_residualconsequences");
        readonly By ResidualConsequence_TitleField = By.XPath("//*[@id='CWField_residualconsequences' and @title = 'Min 1 - Max 5']");
        readonly By ResidualLikelihood_Field = By.Id("CWField_residuallikelihood");
        readonly By ResidualLikelihood_TitleField = By.XPath("//*[@id='CWField_residuallikelihood' and @title = 'Min 1 - Max 5']");
        readonly By ResidualRiskScore_Field = By.Id("CWField_residualriskscore");
        readonly By ResidualRiskCategory_Field = By.Id("CWField_residualriskcategory");
        #endregion

        readonly By InactiveRecordFooterLabel = By.XPath("//label[text()='Active']/following-sibling::span[text() = 'No']");
        #region Action Plans Tab
        readonly By ActionPlansTab = By.Id("CWNavGroup_ActionPlans");

        #endregion

        public OrganisationalRiskManagementRecordPage WaitForRiskRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(OrgRiskRecordIFrame);
            SwitchToIframe(OrgRiskRecordIFrame);


            return this;
        }

        public OrganisationalRiskManagementRecordPage WaitForDisabledRiskRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(OrgRiskRecordIFrame);
            SwitchToIframe(OrgRiskRecordIFrame);

            return this;
        }

        public OrganisationalRiskManagementRecordPage WaitForOrganisationalRiskManagementRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(OrgRiskRecordIFrame);
            this.SwitchToIframe(OrgRiskRecordIFrame);


            return this;
        }

        public OrganisationalRiskManagementRecordPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            ScrollToElement(SaveButton);
            Click(SaveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ClickBackButton()
        {
            WaitForElement(BackButton);
            Click(BackButton);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ClickResponsibleUserLookUpButton()
        {
            WaitForElement(ResponsibleUser_LookupButton);
            Click(ResponsibleUser_LookupButton);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public OrganisationalRiskManagementRecordPage NavigateToActionPlansTab()
        {
            WaitForElement(ActionPlansTab);
            ScrollToElement(ActionPlansTab);
            Click(ActionPlansTab);

            return this;
        }

        public OrganisationalRiskManagementRecordPage NavigateToDetailsTab()
        {
            WaitForElement(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateRiskTypeFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(RiskType_ErrorLabel, ExpectedText);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateCorporateRiskRegisterFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(CorporateRiskRegister_ErrorLabel, ExpectedText);

            return this;
        }


        public OrganisationalRiskManagementRecordPage ValidateResponsibleTeamMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_MandatoryField);
            else
                WaitForElementNotVisible(ResponsibleTeam_MandatoryField, 3);

            return this;
        }


        public OrganisationalRiskManagementRecordPage ValidateRiskTypeMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(RiskType_MandatoryField);
            else
                WaitForElementNotVisible(RiskType_MandatoryField, 3);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateRiskIdentificationDateMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(RiskIdentificationDate_MandatoryField);
            else
                WaitForElementNotVisible(RiskIdentificationDate_MandatoryField, 3);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateStatusMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(RiskStatus_MandatoryField);
            else
                WaitForElementNotVisible(RiskStatus_MandatoryField, 3);

            return this;
        }


        public OrganisationalRiskManagementRecordPage ValidateCorporateRiskRegisterMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CorporateRiskRegister_MandatoryField);
            else
                WaitForElementNotVisible(CorporateRiskRegister_MandatoryField, 3);

            return this;
        }



        #region Risk Details Field population methods

        public OrganisationalRiskManagementRecordPage ClickRiskTypeLookupButton()
        {
            Click(RiskType_LookupButton);

            return this;
        }

        public OrganisationalRiskManagementRecordPage InsertRiskDescription(string RiskDescriptionText)
        {
            WaitForElementVisible(RiskDescription_Field);
            SendKeys(RiskDescription_Field, RiskDescriptionText);

            return this;
        }

        public OrganisationalRiskManagementRecordPage SelectRiskStatus(String OptionToSelect)
        {
            WaitForElementVisible(RiskStatus_Field);
            SelectPicklistElementByText(RiskStatus_Field, OptionToSelect);

            return this;
        }

        public OrganisationalRiskManagementRecordPage SelectCorporateRiskRegisterValue(String OptionToSelect)
        {
            WaitForElementVisible(CorporateRiskRegister_Field);
            SelectPicklistElementByText(CorporateRiskRegister_Field, OptionToSelect);

            return this;
        }

        public OrganisationalRiskManagementRecordPage InsertRiskIdentificationDate(string DateToInsert)
        {
            WaitForElementVisible(RiskIdentificationDate_Field);
            SendKeys(RiskIdentificationDate_Field, DateToInsert);

            return this;
        }

        public OrganisationalRiskManagementRecordPage InsertNextReviewDate(string DateToInsert)
        {
            WaitForElement(NextReviewDate_Field, 5);
            SendKeys(NextReviewDate_Field, DateToInsert);

            return this;
        }

        public OrganisationalRiskManagementRecordPage InsertRiskClosedDate(string DateToInsert)
        {
            WaitForElementVisible(RiskClosedDate_Field);
            SendKeys(RiskClosedDate_Field, DateToInsert);

            return this;
        }

        #endregion

        #region Initial Risk Field population methods

        public OrganisationalRiskManagementRecordPage InsertRiskConsequenceValue(string ConsequenceValueToInsert)
        {
            SendKeys(Consequence_Field, ConsequenceValueToInsert);

            return this;
        }

        public OrganisationalRiskManagementRecordPage InsertRiskLikelihoodValue(string LikelihoodValueToInsert)
        {
            SendKeys(Likelihood_Field, LikelihoodValueToInsert);
            SendKeysWithoutClearing(Likelihood_Field, Keys.Tab);

            return this;
        }

        #endregion

        #region Residual Risk Field population methods
        public OrganisationalRiskManagementRecordPage InsertResidualConsequenceValue(string ConsequenceValueToInsert)
        {
            WaitForElementVisible(ResidualConsequence_Field);
            SendKeys(ResidualConsequence_Field, ConsequenceValueToInsert);

            return this;
        }

        public OrganisationalRiskManagementRecordPage InsertResidualLikelihoodValue(string LikelihoodValueToInsert)
        {
            WaitForElementVisible(ResidualLikelihood_Field);
            SendKeys(ResidualLikelihood_Field, LikelihoodValueToInsert);

            return this;
        }
        #endregion

        #region General Section Field Validation Methods

        public string GetRiskNumber()
        {
            string riskNumber = GetElementValue(RiskNumber_Field);

            return riskNumber;
        }

        public OrganisationalRiskManagementRecordPage ValidateRiskType(string expectedRiskType)
        {
            ValidateElementByTitle(RiskType_Field, expectedRiskType);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateRiskStatusValue(string ExpectedValue)
        {
            ValidatePicklistSelectedText(RiskStatus_Field, ExpectedValue);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateRiskIdentificationField(string expectedValue)
        {
            WaitForElementVisible(RiskIdentificationDate_Field);
            string actualValue = GetElementValue(RiskIdentificationDate_Field);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateNextReviewDateField(string expectedValue)
        {
            WaitForElement(NextReviewDate_Field, 5);
            string actualValue = GetElementValue(NextReviewDate_Field);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateRiskClosedDateField(string expectedValue)
        {
            WaitForElementVisible(RiskClosedDate_Field);
            string actualValue = GetElementValue(RiskClosedDate_Field);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }

        #endregion

        #region Initial Risk Score section field validation methods

        public OrganisationalRiskManagementRecordPage ValidateConsequenceValue(string ExpectedValue)
        {
            ValidateElementValue(Consequence_Field, ExpectedValue);
            string ConsequenceValue = GetElementValue(Consequence_Field);
            Assert.AreEqual(ExpectedValue, ConsequenceValue);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateLikelihoodValue(string ExpectedValue)
        {
            //ValidateElementValue(Likelihood_Field, ExpectedValue);
            string ActualRiskScore = GetElementValue(Likelihood_Field);
            Assert.AreEqual(ExpectedValue, ActualRiskScore);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateInitialRiskScoreValue(string ExpectedValue)
        {
            ValidateElementValue(InitialRiskScore_Field, ExpectedValue);
            string ActualRiskScore = GetElementValue(InitialRiskScore_Field);
            Assert.AreEqual(ExpectedValue, ActualRiskScore);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateInitialRiskCategoryValue(string ExpectedValue)
        {
            ValidateElementValue(RiskCategory_Field, ExpectedValue);
            string ActualRiskCategory = GetElementValue(RiskCategory_Field);
            Assert.AreEqual(ExpectedValue, ActualRiskCategory);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateConsequenceErrorLabelVisibility(bool ExpectedElementVisible)
        {
            if(ExpectedElementVisible)
            {
                WaitForElementVisible(Consequence_ErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Consequence_ErrorLabel, 3);
            }
            

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateConsequenceErrorLabelText(string ExpectedText)
        {
            Assert.AreEqual(ExpectedText, GetElementText(Consequence_ErrorLabel));

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateLikelihoodErrorLabelVisibility(bool ExpectedElementVisible)
        {
            if (ExpectedElementVisible)
            {
                WaitForElementVisible(Likelihood_ErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Likelihood_ErrorLabel, 3);
            }


            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateLikelihoodErrorLabelText(string ExpectedText)
        {
            Assert.AreEqual(ExpectedText, GetElementText(Likelihood_ErrorLabel));

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateInitialConsequenceEnabled(bool ExpectEnabled)
        {
            WaitForElement(Consequence_Field);

            if (ExpectEnabled)
            {
                ValidateElementEnabled(Consequence_Field);
            }
            else
            {
                ValidateElementDisabled(Consequence_Field);
            }

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateInitialLikelihoodEnabled(bool ExpectEnabled)
        {
            WaitForElement(Likelihood_Field);

            if (ExpectEnabled)
            {
                ValidateElementEnabled(Likelihood_Field);
            }
            else
            {
                ValidateElementDisabled(Likelihood_Field);
            }

            return this;
        }

        #endregion

        #region Residual Risk section field validation methods
        public OrganisationalRiskManagementRecordPage ValidateResidualConsequenceValue(string ExpectedValue)
        {
            ValidateElementValue(ResidualConsequence_Field, ExpectedValue);
            string ConsequenceValue = GetElementValue(ResidualConsequence_Field);
            Assert.AreEqual(ExpectedValue, ConsequenceValue);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateResidualLikelihoodValue(string ExpectedValue)
        {
            //ValidateElementValue(Likelihood_Field, ExpectedValue);
            string ActualRiskScore = GetElementValue(ResidualConsequence_Field);
            Assert.AreEqual(ExpectedValue, ActualRiskScore);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateResidualRiskScoreValue(string ExpectedValue)
        {
            ValidateElementValue(ResidualRiskScore_Field, ExpectedValue);
            string ActualRiskScore = GetElementValue(ResidualRiskScore_Field);
            Assert.AreEqual(ExpectedValue, ActualRiskScore);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateResidualRiskCategoryValue(string ExpectedValue)
        {
            ValidateElementValue(ResidualRiskCategory_Field, ExpectedValue);
            string ActualRiskCategory = GetElementValue(ResidualRiskCategory_Field);
            Assert.AreEqual(ExpectedValue, ActualRiskCategory);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateRiskDetailsSectionFields()
        {
            WaitForElement(RiskNumber_Field);
            ValidateElementDisabled(RiskNumber_Field);

            WaitForElement(ResponsibleTeam_Field);
            ValidateElementEnabled(ResponsibleTeam_Field);

            WaitForElement(RiskType_Field);
            ValidateElementEnabled(RiskType_Field);

            WaitForElement(ResponsibleUser_Field);
            ValidateElementEnabled(ResponsibleUser_Field);

            WaitForElement(RiskDescription_Field);
            ValidateElementEnabled(RiskDescription_Field);

            WaitForElement(RiskIdentificationDate_Field);
            ValidateElementEnabled(RiskIdentificationDate_Field);

            WaitForElement(RiskStatus_Field);
            ValidateElementEnabled(RiskStatus_Field);

            WaitForElement(NextReviewDate_Field);
            ValidateElementEnabled(NextReviewDate_Field);

            WaitForElement(CorporateRiskRegister_Field);
            ValidateElementEnabled(CorporateRiskRegister_Field);

            WaitForElement(RiskClosedDate_Field);
            ValidateElementDisabled(RiskClosedDate_Field);

            return this;
        }


        public OrganisationalRiskManagementRecordPage ValidateIntialRiskScoreSectionFields()
        {
            WaitForElement(Consequence_Field);
            ValidateElementEnabled(Consequence_Field);

            WaitForElement(Likelihood_Field);
            ValidateElementEnabled(Likelihood_Field);

            WaitForElement(InitialRiskScore_Field);
            ValidateElementDisabled(InitialRiskScore_Field);

            WaitForElement(RiskCategory_Field);
            ValidateElementDisabled(RiskCategory_Field);

           

            return this;
        }


        public OrganisationalRiskManagementRecordPage ValidateResidualRiskScoreSectionFields()
        {
            WaitForElement(ResidualConsequence_Field);
            ValidateElementEnabled(ResidualConsequence_Field);

            WaitForElement(ResidualLikelihood_Field);
            ValidateElementEnabled(ResidualLikelihood_Field);

            WaitForElement(ResidualRiskScore_Field);
            ValidateElementDisabled(ResidualRiskScore_Field);

            WaitForElement(ResidualRiskCategory_Field);
            ValidateElementDisabled(ResidualRiskCategory_Field);



            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateConsequenceMinMaxScore(string ExpectedText)
        {
            WaitForElement(Consequence_titleField);
            ValidateElementByTitle(Consequence_titleField, ExpectedText);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateLikelihoodMinMaxScore(string ExpectedText)
        {
            WaitForElement(Likelihood_titleField);
            ValidateElementByTitle(Likelihood_titleField, ExpectedText);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateResidualConsequenceMinMaxScore(string ExpectedText)
        {
            WaitForElement(ResidualConsequence_TitleField);
            ValidateElementByTitle(ResidualConsequence_TitleField, ExpectedText);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateResidualLikelihoodMinMaxScore(string ExpectedText)
        {
            WaitForElement(ResidualLikelihood_TitleField);
            ValidateElementByTitle(ResidualLikelihood_TitleField, ExpectedText);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateResidualConsequenceEnabled(bool ExpectEnabled)
        {
            WaitForElement(ResidualConsequence_Field);

            if (ExpectEnabled)
            {
                ValidateElementEnabled(ResidualConsequence_Field);
            }
            else
            {
                ValidateElementDisabled(ResidualConsequence_Field);
            }

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateResidualLikelihoodEnabled(bool ExpectEnabled)
        {
            WaitForElement(ResidualLikelihood_Field);

            if (ExpectEnabled)
            {
                ValidateElementEnabled(ResidualLikelihood_Field);
            }
            else
            {
                ValidateElementDisabled(ResidualLikelihood_Field);
            }

            return this;
        }

        #endregion

        #region Field and Field Label Validation Method
        public OrganisationalRiskManagementRecordPage ValidateCorporateRiskRegisterValue(string ExpectedValue)
        {
            ValidateElementValue(CorporateRiskRegister_Field, ExpectedValue);

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateConsequenceFieldLabelText()
        {
            //ValidateElementText(ConsequenceField_Label, "Consequence");
            Assert.IsTrue(GetElementVisibility(ConsequenceField_Label));
            
            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateRiskClosedDateEnabled()
        {
            WaitForElement(RiskClosedDate_AutopopulatedFieldValue);
            ValidateElementEnabled(RiskClosedDate_AutopopulatedFieldValue);        

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateResidualConsequenceFieldLabelText()
        {
            //ValidateElementText(ResidualConsequenceField_Label, "Residual Consequence");
            Assert.AreEqual("Residual Consequence", GetElementText(ResidualConsequenceField_Label));

            return this;
        }

        public OrganisationalRiskManagementRecordPage ValidateInactiveRecordFooterLabel()
        {
            Assert.IsTrue(GetElementVisibility(InactiveRecordFooterLabel));

            return this;
        }
        
        public OrganisationalRiskManagementRecordPage ValidateRiskClosedAutopopulatedDate(string ExpectedText)
        {
            WaitForElement(RiskClosedDate_Field);
            ScrollToElement(RiskClosedDate_Field);
            ValidateElementText(RiskClosedDate_Field, ExpectedText);



            return this;
        }


        public OrganisationalRiskManagementRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(audit_MenuLeftSubMenu);
            Click(audit_MenuLeftSubMenu);

            return this;
        }


        #endregion


    }
}
