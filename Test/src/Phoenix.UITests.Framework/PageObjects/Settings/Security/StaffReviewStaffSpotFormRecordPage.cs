using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class StaffReviewStaffSpotFormRecordPage : CommonMethods
    {
        public StaffReviewStaffSpotFormRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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
        readonly By editAssessmentSave_Button = By.Id("TI_SaveButton");

        By SpotCheckFormRadioBtn_YesOption(string Label) => By.XPath("//label[contains(text(),'" + Label + "')]/parent::li/following-sibling::li/ul/li[@class='radio-label true-false-li']//label[text()='Yes']");
        By SpotCheckFormRadioBtn_NoOption(string Label) => By.XPath("//label[contains(text(),'" + Label + "')]/parent::li/following-sibling::li/ul/li[@class='radio-label true-false-li']//label[text()='No']");

        readonly By validateForm_Mandatory = By.XPath("//span[text()='Please fill out this field.']/parent::label");


        public StaffReviewStaffSpotFormRecordPage WaitForEditAssessment_StaffReviewStaffSpotFormRecordPageRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(staffReviewForm_IFrame);
            SwitchToIframe(staffReviewForm_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(assessmentDialog_IFrame);
            SwitchToIframe(assessmentDialog_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(editAssessmentSaveAndClose_Button);

            return this;
        }

        public StaffReviewStaffSpotFormRecordPage ClickEditAssessmentSaveAndCloseButton()
        {
            WaitForElementToBeClickable(editAssessmentSaveAndClose_Button);
            Click(editAssessmentSaveAndClose_Button);

            return this;
        }

        public StaffReviewStaffSpotFormRecordPage ClickEditAssessmentSaveButton()
        {
            WaitForElementToBeClickable(editAssessmentSave_Button);
            Click(editAssessmentSave_Button);
            return this;
        }
        public StaffReviewStaffSpotFormRecordPage ValidateSpotCheckFormGeneral_RadioBtn_Visible(String Label)
        {
            WaitForElementVisible(SpotCheckFormRadioBtn_YesOption(Label));
            WaitForElementVisible(SpotCheckFormRadioBtn_NoOption(Label));

            return this;
        }

        public StaffReviewStaffSpotFormRecordPage ClickSpotCheckFormGeneral_YesRadioBtn(String Label)
        {
            WaitForElementVisible(SpotCheckFormRadioBtn_YesOption(Label));
            Click(SpotCheckFormRadioBtn_YesOption(Label));
           

            return this;
        }

        public StaffReviewStaffSpotFormRecordPage ClickSpotCheckFormGeneral_NoRadioBtn(String Label)
        {
            WaitForElementVisible(SpotCheckFormRadioBtn_NoOption(Label));
            Click(SpotCheckFormRadioBtn_NoOption(Label));


            return this;
        }


        public StaffReviewStaffSpotFormRecordPage ValidateMandatoryErrorMsgVisible(bool ExpectVisible)
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
