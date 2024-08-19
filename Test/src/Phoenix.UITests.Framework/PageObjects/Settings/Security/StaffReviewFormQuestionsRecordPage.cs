using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class StaffReviewFormQuestionsRecordPage : CommonMethods
    {
        public StaffReviewFormQuestionsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        readonly By EmployeeJobTitle_TextArea = By.XPath("//*[@id='CW-DQ-31085']");
        readonly By EmployementStartDate_DateFld = By.XPath("//*[@id='CW-DQ-31086']");
        readonly By JobDescription_TextArea = By.XPath("//*[@id='CW-DQ-31087']");
        readonly By ManagerComments_TextArea = By.XPath("//*[@id='CW-DQ-31091']");
        readonly By ManagerComments2_TextArea = By.XPath("//*[@id='CW-DQ-31093']");
        readonly By ManagerComments3_TextArea = By.XPath("//*[@id='CW-DQ-31095']");
        readonly By ManagerComments4_TextArea = By.XPath("//*[@id='CW-DQ-31097']");
        readonly By ManagerComments5_TextArea = By.XPath("//*[@id='CW-DQ-31099']");
        readonly By ManagerComments6_TextArea = By.XPath("//*[@id='CW-DQ-31101']");
        readonly By ManagerComments7_TextArea = By.XPath("//*[@id='CW-DQ-31103']");
        readonly By ManagerComments8_TextArea = By.XPath("//*[@id='CW-DQ-31105']");
        readonly By ManagerComments9_TextArea = By.XPath("//*[@id='CW-DQ-31107']");
        readonly By ManagerComments10_TextArea = By.XPath("//*[@id='CW-DQ-31109']");
        readonly By StaffMemberAchievments_TextArea = By.XPath("//*[@id='CW-DQ-31110']");
        readonly By Why_TextArea = By.XPath("//*[@id='CW-DQ-31111']");
        readonly By DifficultiesMemberStaffEncountered_TextArea = By.XPath("//*[@id='CW-DQ-31112']");
        readonly By ActionToBeTaken_TextArea = By.XPath("//*[@id='CW-DQ-31113']");
        readonly By WatDoIDoBest_TextArea = By.XPath("//*[@id='CW-DQ-31114']");
        readonly By WatDoIDoLessWell_TextArea = By.XPath("//*[@id='CW-DQ-31115']");
        readonly By WatDoIHaveDifficultyWith_TextArea = By.XPath("//*[@id='CW-DQ-31116']");
        readonly By WatDoIFailToEnjoy_TextArea = By.XPath("//*[@id='CW-DQ-31117']");
        readonly By WatWouldMemberstaffToAchieve_TextArea = By.XPath("//*[@id='CW-DQ-31118']");
        readonly By AdditionalTrainingWhy_TextArea = By.XPath("//*[@id='CW-DQ-31119']");
        readonly By WatAdditionalTrainingRequired_TextArea = By.XPath("//*[@id='CW-DQ-31120']");
        readonly By HowWillThisBeAchieved_TextArea = By.XPath("//*[@id='CW-DQ-31121']");
        readonly By Action_TextArea = By.XPath("//*[@name='1-CW-DQ-31172']");
        readonly By AgreedTimeScaleNdate_TextArea = By.XPath("//*[@name='1-CW-DQ-31173']");
        readonly By validateForm_Mandatory = By.XPath("//span[text()='Please fill out this field.']/parent::label");


        By RadioButtonTimeKeepingShiftHours_Text(string Label) => By.XPath("//*[@name='CW-DQ-31090']/parent::span/parent::li//label[text()='" + Label + "']");
        By RadioButtonTimeKeepingShiftHours(string Label) => By.XPath("//label[text()='" + Label + "']/parent::li//*[@name='CW-DQ-31090']");
        By RadioButtonPunctuality(string Label) => By.XPath("//label[text()='" + Label + "']/parent::li//*[@name='CW-DQ-31092']");
        By RadioButtonAppearance(string Label) => By.XPath("//label[text()='" + Label + "']/parent::li//*[@name='CW-DQ-31094']");
        By RadioButtonMannerPoliteness(string Label) => By.XPath("//label[text()='" + Label + "']/parent::li//*[@name='CW-DQ-31096']");
        By RadioButtonComparisionTowardsGuests(string Label) => By.XPath("//label[text()='" + Label + "']/parent::li//*[@name='CW-DQ-31098']");
        By RadioButtonWillingnessToLearn(string Label) => By.XPath("//label[text()='" + Label + "']/parent::li//*[@name='CW-DQ-31100']");
        By RadioButtonDependability(string Label) => By.XPath("//label[text()='" + Label + "']/parent::li//*[@name='CW-DQ-31102']");
        By RadioButtonAbilityToUseOwnInitiative(string Label) => By.XPath("//label[text()='" + Label + "']/parent::li//*[@name='CW-DQ-31104']");
        By RadioButtonTeamWork(string Label) => By.XPath("//label[text()='" + Label + "']/parent::li//*[@name='CW-DQ-31106']");
        By RadioButtonCommunication(string Label) => By.XPath("//label[text()='" + Label + "']/parent::li//*[@name='CW-DQ-31108']");
        By RadioButtonReviewHasBeenAgreed(string Label) => By.XPath("//label[text()='" + Label + "']/parent::li//*[@name='CW-DQ-31134']");

        By RadioButtonPunctuality_Text(string Label) => By.XPath("//*[@name='CW-DQ-31092']/parent::span/parent::li//label[text()='" + Label + "']");
        By RadioButtonAppearance_Text(string Label) => By.XPath("//*[@name='CW-DQ-31094']/parent::span/parent::li//label[text()='" + Label + "']");
        By RadioButtonMannerPoliteness_Text(string Label) => By.XPath("//*[@name='CW-DQ-31096']/parent::span/parent::li//label[text()='" + Label + "']");
        By RadioButtonComparisionTowardsGuests_Text(string Label) => By.XPath("//*[@name='CW-DQ-31098']/parent::span/parent::li//label[text()='" + Label + "']");
        By RadioButtonWillingnessToLearn_Text(string Label) => By.XPath("//*[@name='CW-DQ-31100']/parent::span/parent::li//label[text()='" + Label + "']");
        By RadioButtonDependability_Text(string Label) => By.XPath("//*[@name='CW-DQ-31102']/parent::span/parent::li//label[text()='" + Label + "']");
        By RadioButtonAbilityToUseOwnInitiative_Text(string Label) => By.XPath("//*[@name='CW-DQ-31104']/parent::span/parent::li//label[text()='" + Label + "']");
        By RadioButtonTeamWork_Text(string Label) => By.XPath("//*[@name='CW-DQ-31106']/parent::span/parent::li//label[text()='" + Label + "']");
        By RadioButtonCommunication_Text(string Label) => By.XPath("//*[@name='CW-DQ-31108']/parent::span/parent::li//label[text()='" + Label + "']");

        By RadioButtonReviewHasBeenAgreed_Text(string Label) => By.XPath("//*[@name='CW-DQ-31134']/parent::span/parent::li//label[text()='" + Label + "']");


        By questionIdentifierTextarea_Field(string questionID) => By.XPath("//*[@queidentifierid ='" + questionID + "']");
        By questionIdentifierTextarea_Label(string questionID) => By.XPath("//*[@queidentifierid ='" + questionID + "']/parent::li/preceding-sibling::li/label");
        By questionIdentifierYes_RadioBtn(string text) => By.XPath("//label[text()='" + text + "']/parent::li/following-sibling::ul//li/span/input[@title='Yes']");


        public StaffReviewFormQuestionsRecordPage WaitForEditAssessment_StaffReviewFormPageToLoad()
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

        public StaffReviewFormQuestionsRecordPage ClickEditAssessmentSaveAndCloseButton()
        {
            WaitForElementToBeClickable(editAssessmentSaveAndClose_Button);
            Click(editAssessmentSaveAndClose_Button);

            return this;
        }
        public StaffReviewFormQuestionsRecordPage ValidateJobDescription()
        {
            WaitForElement(EmployeeJobTitle_TextArea);
            Assert.IsTrue(GetElementVisibility(EmployeeJobTitle_TextArea));
            WaitForElement(EmployementStartDate_DateFld);
            Assert.IsTrue(GetElementVisibility(EmployementStartDate_DateFld));
            WaitForElement(JobDescription_TextArea);
            Assert.IsTrue(GetElementVisibility(JobDescription_TextArea));

            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertJobDescription_EmployeeJobTitle(String title)
        {
            WaitForElement(EmployeeJobTitle_TextArea);
            SendKeys(EmployeeJobTitle_TextArea, title);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertJobDescription_EmployementStartDate(String StartDate)
        {

            WaitForElement(EmployementStartDate_DateFld);
            SendKeys(EmployementStartDate_DateFld, StartDate);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertJobDescription_JobDescription(String JobDescription)
        {
            WaitForElement(JobDescription_TextArea);
            SendKeys(JobDescription_TextArea, JobDescription);


            return this;
        }

        public StaffReviewFormQuestionsRecordPage ValidatePersonalQualities_TimeKeepigShiftHrs_RadioButtonVisibility(string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonTimeKeepingShiftHours_Text(Label));
            else
                WaitForElementNotVisible(RadioButtonTimeKeepingShiftHours_Text(Label), 7);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage SelectPersonalQualities_TimeKeepigShiftHrs_RadioButton(string Label)
        {
            ScrollToElement(RadioButtonTimeKeepingShiftHours(Label));
            WaitForElementToBeClickable(RadioButtonTimeKeepingShiftHours(Label));
            Click(RadioButtonTimeKeepingShiftHours(Label));

            return this;
        }

        public StaffReviewFormQuestionsRecordPage ValidateManagerCommentsTextArea()
        {
            WaitForElement(ManagerComments_TextArea);
            Assert.IsTrue((GetElementVisibility(ManagerComments_TextArea)));
            WaitForElement(ManagerComments2_TextArea);
            Assert.IsTrue((GetElementVisibility(ManagerComments2_TextArea)));
            WaitForElement(ManagerComments3_TextArea);
            Assert.IsTrue((GetElementVisibility(ManagerComments3_TextArea)));
            WaitForElement(ManagerComments4_TextArea);
            Assert.IsTrue((GetElementVisibility(ManagerComments4_TextArea)));
            WaitForElement(ManagerComments5_TextArea);
            Assert.IsTrue((GetElementVisibility(ManagerComments5_TextArea)));
            WaitForElement(ManagerComments6_TextArea);
            Assert.IsTrue((GetElementVisibility(ManagerComments6_TextArea)));
            WaitForElement(ManagerComments7_TextArea);
            Assert.IsTrue((GetElementVisibility(ManagerComments7_TextArea)));
            WaitForElement(ManagerComments8_TextArea);
            Assert.IsTrue((GetElementVisibility(ManagerComments8_TextArea)));
            WaitForElement(ManagerComments9_TextArea);
            Assert.IsTrue((GetElementVisibility(ManagerComments9_TextArea)));
            WaitForElement(ManagerComments10_TextArea);
            Assert.IsTrue((GetElementVisibility(ManagerComments10_TextArea)));



            return this;
        }

        public StaffReviewFormQuestionsRecordPage ValidatePersonalQualities_Punctuality_RadioButtonVisibility(string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonPunctuality_Text(Label));
            else
                WaitForElementNotVisible(RadioButtonPunctuality_Text(Label), 7);

            return this;
        }
        public StaffReviewFormQuestionsRecordPage SelectPersonalQualities_Punctuality_RadioButton(string Label)
        {
            WaitForElementToBeClickable(RadioButtonPunctuality(Label));
            Click(RadioButtonPunctuality(Label));

            return this;
        }
        public StaffReviewFormQuestionsRecordPage ValidatePersonalQualities_Appearance_RadioButtonVisibility(string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonAppearance_Text(Label));
            else
                WaitForElementNotVisible(RadioButtonAppearance_Text(Label), 7);

            return this;
        }
        public StaffReviewFormQuestionsRecordPage SelectPersonalQualities_Appearance_RadioButton(string Label)
        {
            WaitForElementToBeClickable(RadioButtonAppearance(Label));
            Click(RadioButtonAppearance(Label));

            return this;
        }
        public StaffReviewFormQuestionsRecordPage ValidatePersonalQualities_MannerPoliteness_RadioButtonVisibility(string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonMannerPoliteness_Text(Label));
            else
                WaitForElementNotVisible(RadioButtonMannerPoliteness_Text(Label), 7);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage SelectPersonalQualities_MannerPoliteness_RadioButton(string Label)
        {
            WaitForElementToBeClickable(RadioButtonMannerPoliteness(Label));
            Click(RadioButtonMannerPoliteness(Label));

            return this;
        }

        public StaffReviewFormQuestionsRecordPage ValidatePersonalQualities_ComparisionTowardsGuests_RadioButtonVisibility(string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonComparisionTowardsGuests_Text(Label));
            else
                WaitForElementNotVisible(RadioButtonComparisionTowardsGuests_Text(Label), 7);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage SelectPersonalQualities_ComparisionTowardsGuests_RadioButton(string Label)
        {
            WaitForElementToBeClickable(RadioButtonComparisionTowardsGuests(Label));
            Click(RadioButtonComparisionTowardsGuests(Label));

            return this;
        }
        public StaffReviewFormQuestionsRecordPage ValidatePersonalQualities_WillingnessToLearn_RadioButtonVisibility(string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonWillingnessToLearn_Text(Label));
            else
                WaitForElementNotVisible(RadioButtonWillingnessToLearn_Text(Label), 7);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage SelectPersonalQualities_WillingnessToLearn_RadioButton(string Label)
        {
            WaitForElementToBeClickable(RadioButtonWillingnessToLearn(Label));
            Click(RadioButtonWillingnessToLearn(Label));

            return this;
        }

        public StaffReviewFormQuestionsRecordPage ValidatePersonalQualities_Dependability_RadioButtonVisibility(string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonDependability_Text(Label));
            else
                WaitForElementNotVisible(RadioButtonDependability_Text(Label), 7);

            return this;
        }
        public StaffReviewFormQuestionsRecordPage SelectPersonalQualities_Dependability_RadioButton(string Label)
        {
            WaitForElementToBeClickable(RadioButtonDependability(Label));
            Click(RadioButtonDependability(Label));

            return this;
        }
        public StaffReviewFormQuestionsRecordPage ValidatePersonalQualities_AbilityToUseOwnInitiative_RadioButtonVisibility(string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonAbilityToUseOwnInitiative_Text(Label));
            else
                WaitForElementNotVisible(RadioButtonAbilityToUseOwnInitiative_Text(Label), 7);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage SelectPersonalQualities_AbilityToUseOwnInitiative_RadioButton(string Label)
        {
            WaitForElementToBeClickable(RadioButtonAbilityToUseOwnInitiative(Label));
            Click(RadioButtonAbilityToUseOwnInitiative(Label));

            return this;
        }
        public StaffReviewFormQuestionsRecordPage ValidatePersonalQualities_TeamWork_RadioButtonVisibility(string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonTeamWork_Text(Label));
            else
                WaitForElementNotVisible(RadioButtonTeamWork_Text(Label), 7);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage SelectPersonalQualities_TeamWork_RadioButton(string Label)
        {
            WaitForElementToBeClickable(RadioButtonTeamWork(Label));
            Click(RadioButtonTeamWork(Label));

            return this;
        }

        public StaffReviewFormQuestionsRecordPage ValidatePersonalQualities_Communication_RadioButtonVisibility(string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonCommunication_Text(Label));
            else
                WaitForElementNotVisible(RadioButtonCommunication_Text(Label), 7);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage SelectPersonalQualities_Communication_RadioButton(string Label)
        {
            WaitForElementToBeClickable(RadioButtonCommunication(Label));
            Click(RadioButtonCommunication(Label));

            return this;
        }

        public StaffReviewFormQuestionsRecordPage ValidateAchievmentsObstaclesAndActionsTextArea()
        {
            WaitForElement(StaffMemberAchievments_TextArea);
            Assert.IsTrue((GetElementVisibility(StaffMemberAchievments_TextArea)));
            WaitForElement(Why_TextArea);
            Assert.IsTrue((GetElementVisibility(Why_TextArea)));
            WaitForElement(DifficultiesMemberStaffEncountered_TextArea);
            Assert.IsTrue((GetElementVisibility(DifficultiesMemberStaffEncountered_TextArea)));
            WaitForElement(ActionToBeTaken_TextArea);
            Assert.IsTrue((GetElementVisibility(ActionToBeTaken_TextArea)));


            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertAcheivmentsObstaclesAndActions_StaffMemberAchievmentsTextArea(string StaffMemberAchievments)
        {
            WaitForElement(StaffMemberAchievments_TextArea);
            SendKeys(StaffMemberAchievments_TextArea, StaffMemberAchievments);



            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertAcheivmentsObstaclesAndActions_WhyTextArea(string Why)
        {

            WaitForElement(Why_TextArea);
            SendKeys(Why_TextArea, Why);



            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertAcheivmentsObstaclesAndActions_DifficultiesMemberStaffEncounteredTextArea(string DifficultiesMemberstaffEncountered)
        {

            WaitForElement(DifficultiesMemberStaffEncountered_TextArea);
            SendKeys(DifficultiesMemberStaffEncountered_TextArea, DifficultiesMemberstaffEncountered);



            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertAcheivmentsObstaclesAndActions_ActionToBeTakenTextArea(string ActionToBeTaken)
        {

            WaitForElement(ActionToBeTaken_TextArea);
            SendKeys(ActionToBeTaken_TextArea, ActionToBeTaken);


            return this;
        }


        public StaffReviewFormQuestionsRecordPage ValidateAdditionalTrainingTextArea()
        {
            WaitForElement(WatDoIDoBest_TextArea);
            WaitForElement(WatDoIDoLessWell_TextArea);
            WaitForElement(WatDoIHaveDifficultyWith_TextArea);
            WaitForElement(WatDoIFailToEnjoy_TextArea);
            WaitForElement(WatWouldMemberstaffToAchieve_TextArea);
            WaitForElement(AdditionalTrainingWhy_TextArea);
            WaitForElement(WatAdditionalTrainingRequired_TextArea);
            WaitForElement(HowWillThisBeAchieved_TextArea);


            return this;
        }
        public StaffReviewFormQuestionsRecordPage InsertAdditionalTraining_WatDoIDoBestTextArea(string wattodobest)
        {
            WaitForElement(WatDoIDoBest_TextArea);
            SendKeys(WatDoIDoBest_TextArea, wattodobest);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertAdditionalTraining_WatDoIDoLessWellTextArea(string WatDoIDoLessWell)
        {

            WaitForElement(WatDoIDoLessWell_TextArea);
            SendKeys(WatDoIDoLessWell_TextArea, WatDoIDoLessWell);
            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertAdditionalTraining_WatDoIHaveDifficultyWithTextArea(string WatDoIHaveDifficultyWith)
        {


            WaitForElement(WatDoIHaveDifficultyWith_TextArea);
            SendKeys(WatDoIHaveDifficultyWith_TextArea, WatDoIHaveDifficultyWith);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertAdditionalTraining_WatDoIFailToEnjoyTextArea(string WatDoIFailToEnjoy)
        {

            WaitForElement(WatDoIFailToEnjoy_TextArea);
            SendKeys(WatDoIDoBest_TextArea, WatDoIFailToEnjoy);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertAdditionalTraining_WatWouldMemberstaffToAchieveTextArea(string WatWouldMemberstaffToAchieve)
        {


            WaitForElement(WatWouldMemberstaffToAchieve_TextArea);
            SendKeys(WatWouldMemberstaffToAchieve_TextArea, WatWouldMemberstaffToAchieve);


            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertAdditionalTraining_AdditionalTrainingWhyTextArea(string AdditionalTrainingWhy)
        {
            WaitForElement(AdditionalTrainingWhy_TextArea);
            SendKeys(AdditionalTrainingWhy_TextArea, AdditionalTrainingWhy);


            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertAdditionalTraining_WatAdditionalTrainingRequiredTextArea(string WatAdditionalTrainingRequired)
        {

            WaitForElement(WatAdditionalTrainingRequired_TextArea);
            SendKeys(WatAdditionalTrainingRequired_TextArea, WatAdditionalTrainingRequired);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertAdditionalTraining_HowWillThisBeAchievedTextArea(string HowWillThisBeAchieved)
        {
            WaitForElement(HowWillThisBeAchieved_TextArea);
            SendKeys(HowWillThisBeAchieved_TextArea, HowWillThisBeAchieved);

            return this;
        }
        public StaffReviewFormQuestionsRecordPage ValidateActionPlanSignOffTextArea()
        {
            WaitForElement(Action_TextArea);
            WaitForElement(AgreedTimeScaleNdate_TextArea);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage InsertActionPlanSignOffTextArea(string ActionText, string AgreedTimeScaleNdate)
        {
            WaitForElement(Action_TextArea);
            SendKeys(Action_TextArea, ActionText);
            WaitForElement(AgreedTimeScaleNdate_TextArea);
            SendKeys(AgreedTimeScaleNdate_TextArea, AgreedTimeScaleNdate);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage ValidateActionPlanNSignOff_ReviewHasBeenAgreed_RadioButtonVisibility(string Label, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RadioButtonReviewHasBeenAgreed_Text(Label));
            else
                WaitForElementNotVisible(RadioButtonReviewHasBeenAgreed_Text(Label), 7);

            return this;
        }

        public StaffReviewFormQuestionsRecordPage SelectActionPlanNSignOff_ReviewHasBeenAgreed_RadioButton(string Label)
        {
            WaitForElementToBeClickable(RadioButtonReviewHasBeenAgreed(Label));
            Click(RadioButtonReviewHasBeenAgreed(Label));

            return this;
        }

        public StaffReviewFormQuestionsRecordPage ValidateMandatoryErrorMsgVisible(bool ExpectVisible)
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
