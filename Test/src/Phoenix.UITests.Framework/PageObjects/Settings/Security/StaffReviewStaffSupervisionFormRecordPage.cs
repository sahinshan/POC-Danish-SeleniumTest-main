using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class StaffReviewStaffSupervisionFormRecordPage : CommonMethods
    {
        public StaffReviewStaffSupervisionFormRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        readonly By Strengths_TextArea = By.XPath("//*[@id='CW-DQ-31238']");
        readonly By Weakness_TextArea = By.XPath("//*[@id='CW-DQ-31239']");
        readonly By TargetsAchieved_TextArea = By.XPath("//*[@id='CW-DQ-31240']");
        readonly By ProblemsEncountered_TextArea = By.XPath("//*[@id='CW-DQ-31241']");
        readonly By TrainingRequirements_TextArea = By.XPath("//*[@id='CW-DQ-31242']");
        readonly By ObjectSetForNextSupervision_TextArea = By.XPath("//*[@id='CW-DQ-31243']");

        readonly By validateForm_Mandatory = By.XPath("//span[text()='Please fill out this field.']/parent::label");


        public StaffReviewStaffSupervisionFormRecordPage WaitForEditAssessment_StaffSupervisionFormRecordPageToLoad()
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

        public StaffReviewStaffSupervisionFormRecordPage ClickEditAssessmentSaveAndCloseButton()
        {
            WaitForElementToBeClickable(editAssessmentSaveAndClose_Button);
            Click(editAssessmentSaveAndClose_Button);

            return this;
        }
        public StaffReviewStaffSupervisionFormRecordPage VerifyStrengths_TextAreaVisible()
        {
            WaitForElement(Strengths_TextArea);
            Assert.IsTrue(GetElementVisibility(Strengths_TextArea));
            return this;
        }

        public StaffReviewStaffSupervisionFormRecordPage InsertStrengths_TextArea(string keys)
        {
            WaitForElement(Strengths_TextArea);
            SendKeys(Strengths_TextArea,keys);

            return this;
        }

        public StaffReviewStaffSupervisionFormRecordPage VerifyWeakness_TextAreaVisible()
        {
            WaitForElement(Weakness_TextArea);
            Assert.IsTrue(GetElementVisibility(Weakness_TextArea));
            return this;
        }
        public StaffReviewStaffSupervisionFormRecordPage InsertWeakness_TextArea(string keys)
        {
            WaitForElement(Weakness_TextArea);
            SendKeys(Weakness_TextArea, keys);

            return this;
        }
        public StaffReviewStaffSupervisionFormRecordPage VerifyTargetsAchieved_TextAreaVisible()
        {
            WaitForElement(TargetsAchieved_TextArea);
            Assert.IsTrue(GetElementVisibility(TargetsAchieved_TextArea));
            return this;
        }
        public StaffReviewStaffSupervisionFormRecordPage InsertTargetsAchieved_TextArea(string keys)
        {
            WaitForElement(TargetsAchieved_TextArea);
            SendKeys(TargetsAchieved_TextArea, keys);

            return this;
        }
        public StaffReviewStaffSupervisionFormRecordPage VerifyProblemsEncountered_TextAreaVisible()
        {
            WaitForElement(ProblemsEncountered_TextArea);
            Assert.IsTrue(GetElementVisibility(ProblemsEncountered_TextArea));
            return this;
        }
        public StaffReviewStaffSupervisionFormRecordPage InsertProblemsEncountered_TextArea(string keys)
        {
            WaitForElement(ProblemsEncountered_TextArea);
            SendKeys(ProblemsEncountered_TextArea, keys);

            return this;
        }

        public StaffReviewStaffSupervisionFormRecordPage VerifyTrainingRequirements_TextAreaVisible()
        {
            WaitForElement(TrainingRequirements_TextArea);
            Assert.IsTrue(GetElementVisibility(TrainingRequirements_TextArea));
            return this;
        }

        public StaffReviewStaffSupervisionFormRecordPage InsertTrainingRequirements_TextArea(string keys)
        {
            WaitForElement(TrainingRequirements_TextArea);
            SendKeys(TrainingRequirements_TextArea, keys);

            return this;
        }
        public StaffReviewStaffSupervisionFormRecordPage VerifyObjectsetForNextSupervision_TextAreaVisible()
        {
            WaitForElement(ObjectSetForNextSupervision_TextArea);
            Assert.IsTrue(GetElementVisibility(ObjectSetForNextSupervision_TextArea));
            return this;
        }
        public StaffReviewStaffSupervisionFormRecordPage InsertObjectsetForNextSupervision_TextArea(string keys)
        {
            WaitForElement(ObjectSetForNextSupervision_TextArea);
            SendKeys(ObjectSetForNextSupervision_TextArea, keys);

            return this;
        }
        public StaffReviewStaffSupervisionFormRecordPage ValidateMandatoryErrorMsgVisible(bool ExpectVisible)
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
