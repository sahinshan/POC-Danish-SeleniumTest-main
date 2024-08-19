using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class StaffEvaluationFormQuestionsRecordPage : CommonMethods
    {
        public StaffEvaluationFormQuestionsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By staffReviewForm_IFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=staffreviewform&')]");
        readonly By assessmentDialog_IFrame = By.Id("iframe_CWAssessmentDialog");


        readonly By editAssessmentButton = By.XPath("//*[@id='TI_EditAssessmentButton']");
        readonly By editAssessmentSaveAndClose_Button = By.Id("TI_CWAssessmentSaveAndCloseButton");

        readonly By TimeKeeping_selectfield = By.XPath("//*[@id='CW-DQ-31135']");
        readonly By SicknessRAbsence_selectfield = By.XPath("//*[@id='CW-DQ-31136']");
        readonly By AuthorisedAbsence_selectfield = By.XPath("//*[@id='CW-DQ-31137']");
        readonly By AppearanceRAbsence_selectfield = By.XPath("//*[@id='CW-DQ-31138']");
        readonly By MannerRPoliteness_selectfield = By.XPath("//*[@id='CW-DQ-31139']");
        readonly By CompationRAtitude_selectfield = By.XPath("//*[@id='CW-DQ-31140']");
        readonly By HonestyRIntegrity_selectfield = By.XPath("//*[@id='CW-DQ-31141']");
        readonly By TechnicalKnowledge_selectfield = By.XPath("//*[@id='CW-DQ-31142']");
        readonly By QualityOfWork_selectfield = By.XPath("//*[@id='CW-DQ-31143']");
        readonly By FlexibilityTowardsDuties_selectfield = By.XPath("//*[@id='CW-DQ-31144']");
        readonly By AbilityToWorkOnOwn_selectfield = By.XPath("//*[@id='CW-DQ-31145']");
        readonly By AbilityToWorkInTeam_selectfield = By.XPath("//*[@id='CW-DQ-31146']");
        readonly By AbilityToDealWith_selectfield = By.XPath("//*[@id='CW-DQ-31147']");
        readonly By WillingnessToLearn_selectfield = By.XPath("//*[@id='CW-DQ-31148']");
        readonly By AbilityToFollowInstructions_selectfield = By.XPath("//*[@id='CW-DQ-31149']");
        readonly By AchievmentsOftarget_selectfield = By.XPath("//*[@id='CW-DQ-31150']");
        readonly By AcceptanceOfResponsibilities_selectfield = By.XPath("//*[@id='CW-DQ-31151']");
        readonly By AttitudeTowardsStaffNSeniors_selectfield = By.XPath("//*[@id='CW-DQ-31152']");
        readonly By KnowledgeOfJobRelatedPolicies_selectfield = By.XPath("//*[@id='CW-DQ-31153']");


        readonly By validateForm_Mandatory = By.XPath("//span[text()='Please fill out this field.']/parent::label");


        public StaffEvaluationFormQuestionsRecordPage WaitForEditAssessment_StaffEvaluationFormPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(staffReviewForm_IFrame);
            SwitchToIframe(staffReviewForm_IFrame);

            WaitForElement(assessmentDialog_IFrame);
            SwitchToIframe(assessmentDialog_IFrame);

            WaitForElement(editAssessmentSaveAndClose_Button);

            WaitForElementNotVisible("CWRefreshPanel", 20);
            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage ClickEditAssessmentSaveAndCloseButton()
        {
            WaitForElementToBeClickable(editAssessmentSaveAndClose_Button);
            Click(editAssessmentSaveAndClose_Button);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_TimeKeeping_FieldOption(string text)
        {
            WaitForElement(TimeKeeping_selectfield);
            SelectPicklistElementByText(TimeKeeping_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_TimeKeeping_FieldOption(string text)
        {
            WaitForElement(TimeKeeping_selectfield);
            ValidatePicklistContainsElementByText(TimeKeeping_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_SicknessRAbsence_FieldOption(string text)
        {
            WaitForElement(SicknessRAbsence_selectfield);
            SelectPicklistElementByText(SicknessRAbsence_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_SicknessRAbsence_FieldOption(string text)
        {
            WaitForElement(SicknessRAbsence_selectfield);
            ValidatePicklistContainsElementByText(SicknessRAbsence_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_AuthorisedAbsence_FieldOption(string text)
        {
            WaitForElement(AuthorisedAbsence_selectfield);
            SelectPicklistElementByText(AuthorisedAbsence_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_AuthorisedAbsence_FieldOption(string text)
        {
            WaitForElement(AuthorisedAbsence_selectfield);
            ValidatePicklistContainsElementByText(AuthorisedAbsence_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_AppearanceRSmartness_FieldOption(string text)
        {
            WaitForElement(AppearanceRAbsence_selectfield);
            SelectPicklistElementByText(AppearanceRAbsence_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_AppearanceRSmartness_FieldOption(string text)
        {
            WaitForElement(AppearanceRAbsence_selectfield);
            ValidatePicklistContainsElementByText(AppearanceRAbsence_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_MannerPoliteness_FieldOption(string text)
        {
            WaitForElement(MannerRPoliteness_selectfield);
            SelectPicklistElementByText(MannerRPoliteness_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_MannerPoliteness_FieldOption(string text)
        {
            WaitForElement(MannerRPoliteness_selectfield);
            ValidatePicklistContainsElementByText(MannerRPoliteness_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_CompassionAtitude_FieldOption(string text)
        {
            WaitForElement(CompationRAtitude_selectfield);
            SelectPicklistElementByText(CompationRAtitude_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_CompassionAtitude_FieldOption(string text)
        {
            WaitForElement(CompationRAtitude_selectfield);
            ValidatePicklistContainsElementByText(CompationRAtitude_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_HonestyIntegrity_FieldOption(string text)
        {
            WaitForElement(HonestyRIntegrity_selectfield);
            SelectPicklistElementByText(HonestyRIntegrity_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_HonestyIntegrity_FieldOption(string text)
        {
            WaitForElement(HonestyRIntegrity_selectfield);
            ValidatePicklistContainsElementByText(HonestyRIntegrity_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_TechnicalKnowledge_FieldOption(string text)
        {
            WaitForElement(TechnicalKnowledge_selectfield);
            SelectPicklistElementByText(TechnicalKnowledge_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_TechnicalKnowledge_FieldOption(string text)
        {
            WaitForElement(TechnicalKnowledge_selectfield);
            ValidatePicklistContainsElementByText(TechnicalKnowledge_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_QualityOfWork_FieldOption(string text)
        {
            WaitForElement(QualityOfWork_selectfield);
            SelectPicklistElementByText(QualityOfWork_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_QualityOfWork_FieldOption(string text)
        {
            WaitForElement(QualityOfWork_selectfield);
            ValidatePicklistContainsElementByText(QualityOfWork_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_FlexibilityTowardsDuties_FieldOption(string text)
        {
            WaitForElement(FlexibilityTowardsDuties_selectfield);
            SelectPicklistElementByText(FlexibilityTowardsDuties_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_FlexibilityTowardsDuties_FieldOption(string text)
        {
            WaitForElement(FlexibilityTowardsDuties_selectfield);
            ValidatePicklistContainsElementByText(FlexibilityTowardsDuties_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_AbilityToWorkOnOwnInitiative_FieldOption(string text)
        {
            WaitForElement(AbilityToWorkOnOwn_selectfield);
            SelectPicklistElementByText(AbilityToWorkOnOwn_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_AbilityToWorkOnOwnInitiative_FieldOption(string text)
        {
            WaitForElement(AbilityToWorkOnOwn_selectfield);
            ValidatePicklistContainsElementByText(AbilityToWorkOnOwn_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_AbilityToWorkInTeams_FieldOption(string text)
        {
            WaitForElement(AbilityToWorkInTeam_selectfield);
            SelectPicklistElementByText(AbilityToWorkInTeam_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_AbilityToWorkInTeams_FieldOption(string text)
        {
            WaitForElement(AbilityToWorkInTeam_selectfield);
            ValidatePicklistContainsElementByText(AbilityToWorkInTeam_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_AbilityToDealWithProblems_FieldOption(string text)
        {
            WaitForElement(AbilityToDealWith_selectfield);
            SelectPicklistElementByText(AbilityToDealWith_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_AbilityToDealWithProblems_FieldOption(string text)
        {
            WaitForElement(AbilityToDealWith_selectfield);
            ValidatePicklistContainsElementByText(AbilityToDealWith_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_WillingnessToLearn_FieldOption(string text)
        {
            WaitForElement(WillingnessToLearn_selectfield);
            SelectPicklistElementByText(WillingnessToLearn_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_WillingnessToLearn_FieldOption(string text)
        {
            WaitForElement(WillingnessToLearn_selectfield);
            ValidatePicklistContainsElementByText(WillingnessToLearn_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_AbilityToFollowInstructions_FieldOption(string text)
        {
            WaitForElement(AbilityToFollowInstructions_selectfield);
            SelectPicklistElementByText(AbilityToFollowInstructions_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_AbilityToFollowInstructions_FieldOption(string text)
        {
            WaitForElement(AbilityToFollowInstructions_selectfield);
            ValidatePicklistContainsElementByText(AbilityToFollowInstructions_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_AchievmentsOfTargets_FieldOption(string text)
        {
            WaitForElement(AchievmentsOftarget_selectfield);
            SelectPicklistElementByText(AchievmentsOftarget_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_AchievmentsOfTargets_FieldOption(string text)
        {
            WaitForElement(AchievmentsOftarget_selectfield);
            ValidatePicklistContainsElementByText(AchievmentsOftarget_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_AcceptanceOfResponsibilities_FieldOption(string text)
        {
            WaitForElement(AcceptanceOfResponsibilities_selectfield);
            SelectPicklistElementByText(AcceptanceOfResponsibilities_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_AcceptanceOfResponsibilities_FieldOption(string text)
        {
            WaitForElement(AcceptanceOfResponsibilities_selectfield);
            ValidatePicklistContainsElementByText(AcceptanceOfResponsibilities_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_AttitudeTowardsStaffNSeniors_FieldOption(string text)
        {
            WaitForElement(AttitudeTowardsStaffNSeniors_selectfield);
            SelectPicklistElementByText(AttitudeTowardsStaffNSeniors_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_AttitudeTowardsStaffNSeniors_FieldOption(string text)
        {
            WaitForElement(AttitudeTowardsStaffNSeniors_selectfield);
            ValidatePicklistContainsElementByText(AttitudeTowardsStaffNSeniors_selectfield, text);

            return this;
        }

        public StaffEvaluationFormQuestionsRecordPage Select_KnowledgeOfJobRelatedPolicies_FieldOption(string text)
        {
            WaitForElement(KnowledgeOfJobRelatedPolicies_selectfield);
            SelectPicklistElementByText(KnowledgeOfJobRelatedPolicies_selectfield, text);

            return this;
        }



        public StaffEvaluationFormQuestionsRecordPage Validate_KnowledgeOfJobRelatedPolicies_FieldOption(string text)
        {
            WaitForElement(KnowledgeOfJobRelatedPolicies_selectfield);
            ValidatePicklistContainsElementByText(KnowledgeOfJobRelatedPolicies_selectfield, text);

            return this;
        }
        public StaffEvaluationFormQuestionsRecordPage ValidateMandatoryErrorMsgVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(validateForm_Mandatory);
            }
            else
            {
                WaitForElementNotVisible(validateForm_Mandatory, 3);
            }
            return this;
        }
    }
}
