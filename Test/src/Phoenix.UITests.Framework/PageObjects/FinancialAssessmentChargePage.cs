
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinancialAssessmentChargePage : CommonMethods
    {
        public FinancialAssessmentChargePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By financialAssessmentRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=financialassessmentcharge&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Financial Assessment Charge: ']");

        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By viewChargeScheduleButton = By.Id("TI_ViewChargeSchedule");


        #region Fields Labels

        readonly By financialAssessmentLabel = By.XPath("//*[@id='CWLabelHolder_financialassessmentid']/label[text()='Financial Assessment']");
        readonly By responsibleTeamLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");

        readonly By startDateLabel = By.XPath("//*[@id='CWLabelHolder_startdate']/label[text()='Start Date']");
        readonly By endDateLabel = By.XPath("//*[@id='CWLabelHolder_enddate']/label[text()='End Date']");

        readonly By fullCostLabel = By.XPath("//*[@id='CWLabelHolder_isfullcost']/label[text()='Full Cost?']");
        readonly By initialChargeLabel = By.XPath("//*[@id='CWLabelHolder_initialcharge']/label[text()='Initial Charge']");
        readonly By chargeToPayNowLabel = By.XPath("//*[@id='CWLabelHolder_chargetopaynow']/label[text()='Charge - To pay now']");
        readonly By deferredLabel = By.XPath("//*[@id='CWLabelHolder_isdeferred']/label[text()='Deferred?']");
        readonly By limitToMaximumLabel = By.XPath("//*[@id='CWLabelHolder_limittomaximum']/label[text()='Limit To Maximum?']");
        readonly By statusReasonLabel = By.XPath("//*[@id='CWLabelHolder_statusid']/label[text()='Status Reason']");

        readonly By scheduleTypeLabel = By.XPath("//*[@id='CWLabelHolder_scheduletypeid']/label[text()='Schedule Type']");
        readonly By finalChargeLabel = By.XPath("//*[@id='CWLabelHolder_finalcharge']/label[text()='Final Charge']");
        readonly By chargeDeferredLabel = By.XPath("//*[@id='CWLabelHolder_chargedeferred']/label[text()='Charge - Deferred']");
        readonly By manualDeferredLabel = By.XPath("//*[@id='CWLabelHolder_ismanualdeferred']/label[text()='Manual Deferred']");
        readonly By roundDownLabel = By.XPath("//*[@id='CWLabelHolder_rounddown']/label[text()='Round Down?']");
        readonly By cancelledDateLabel = By.XPath("//*[@id='CWLabelHolder_cancelleddate']/label[text()='Cancelled Date']");

        #endregion

        #region Fields

        readonly By financialAssessmentLink = By.XPath("//*[@id='CWField_financialassessmentid_Link']");
        readonly By responsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");

        readonly By startDateField = By.XPath("//*[@id='CWField_startdate']");
        readonly By endDateField = By.XPath("//*[@id='CWField_enddate']");

        readonly By fullCostYesRadiobutton = By.XPath("//*[@id='CWField_isfullcost_1']");
        readonly By fullCostNoRadiobutton = By.XPath("//*[@id='CWField_isfullcost_0']");
        readonly By initialChargeField = By.XPath("//*[@id='CWField_initialcharge']");
        readonly By chargeToPayNowField = By.XPath("//*[@id='CWField_chargetopaynow']");
        readonly By deferredYesRadioButton = By.XPath("//*[@id='CWField_isdeferred_1']");
        readonly By deferredNoRadioButton = By.XPath("//*[@id='CWField_isdeferred_0']");
        readonly By limitToMaximumYesRadioButton = By.XPath("//*[@id='CWField_limittomaximum_1']");
        readonly By limitToMaximumNoRadioButton = By.XPath("//*[@id='CWField_limittomaximum_0']");
        readonly By statusReasonField = By.XPath("//*[@id='CWField_statusid']");

        readonly By scheduleTypeLink = By.XPath("//*[@id='CWField_scheduletypeid_Link']");
        readonly By finalChargeField = By.XPath("//*[@id='CWField_finalcharge']");
        readonly By chargeDeferredField = By.XPath("//*[@id='CWField_cargedeferred']");
        readonly By manualDeferredYesRadioButton = By.XPath("//*[@id='CWField_ismanualdeferred_1']");
        readonly By manualDeferredNoRadioButton = By.XPath("//*[@id='CWField_ismanualdeferred_0']");
        readonly By roundDownYesRadioButton = By.XPath("//*[@id='CWField_rounddown_1']");
        readonly By roundDownNoRadioButton = By.XPath("//*[@id='CWField_rounddown_0']");
        readonly By cancelledDateField = By.XPath("//*[@id='CWField_cancelleddate']");

        #endregion


        public FinancialAssessmentChargePage WaitForFinancialAssessmentRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(financialAssessmentRecordIFrame);
            SwitchToIframe(financialAssessmentRecordIFrame);

            WaitForElement(pagehehader);

            WaitForElement(financialAssessmentLabel);
            WaitForElement(responsibleTeamLabel);

            WaitForElement(startDateLabel);
            WaitForElement(endDateLabel);

            WaitForElement(fullCostLabel);
            WaitForElement(initialChargeLabel);
            WaitForElement(chargeToPayNowLabel);
            WaitForElement(deferredLabel);
            WaitForElement(limitToMaximumLabel);
            WaitForElement(statusReasonLabel);

            WaitForElement(scheduleTypeLabel);
            WaitForElement(finalChargeLabel);
            WaitForElement(chargeDeferredLabel);
            WaitForElement(manualDeferredLabel);
            WaitForElement(roundDownLabel);
            WaitForElement(cancelledDateLabel);


            return this;
        }

        public FinancialAssessmentChargePage TapBackButton()
        {
            Click(backButton);

            return this;
        }

        public FinancialAssessmentChargePage ValidateStartDateValue(string ExpectedValue)
        {
            ValidateElementValue(startDateField, ExpectedValue);

            return this;
        }

        public FinancialAssessmentChargePage ValidateEndDateValue(string ExpectedValue)
        {
            ValidateElementValue(endDateField, ExpectedValue);

            return this;
        }

        public FinancialAssessmentChargePage ValidateFullCost(bool FullCost)
        {
            if (FullCost)
            {
                ValidateElementChecked(fullCostYesRadiobutton);
                ValidateElementNotChecked(fullCostNoRadiobutton);
            }
            else
            {
                ValidateElementNotChecked(fullCostYesRadiobutton);
                ValidateElementChecked(fullCostNoRadiobutton);
            }

            return this;
        }

        public FinancialAssessmentChargePage ValidateInitialChargeValue(string ExpectedValue)
        {
            ValidateElementValue(initialChargeField, ExpectedValue);

            return this;
        }

        public FinancialAssessmentChargePage ValidateChargeToPayNowValue(string ExpectedValue)
        {
            ValidateElementValue(chargeToPayNowField, ExpectedValue);

            return this;
        }

        public FinancialAssessmentChargePage ValidateDeferred(bool Deferred)
        {
            if (Deferred)
            {
                ValidateElementChecked(deferredYesRadioButton);
                ValidateElementNotChecked(deferredNoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(deferredYesRadioButton);
                ValidateElementChecked(deferredNoRadioButton);
            }

            return this;
        }

        public FinancialAssessmentChargePage ValidateLimitToMaximum(bool LimitToMaximum)
        {
            if (LimitToMaximum)
            {
                ValidateElementChecked(limitToMaximumYesRadioButton);
                ValidateElementNotChecked(limitToMaximumNoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(limitToMaximumYesRadioButton);
                ValidateElementChecked(limitToMaximumNoRadioButton);
            }

            return this;
        }

        public FinancialAssessmentChargePage ValidateStatusReasonValue(string ExpectedValue)
        {
            ValidateElementValue(statusReasonField, ExpectedValue);

            return this;
        }

        public FinancialAssessmentChargePage ValidateScheduleTypeValue(string ExpectedText)
        {
            ValidateElementText(scheduleTypeLink, ExpectedText);

            return this;
        }

        public FinancialAssessmentChargePage ValidateFinalChargeValue(string ExpectedValue)
        {
            ValidateElementValue(finalChargeField, ExpectedValue);

            return this;
        }

        public FinancialAssessmentChargePage ValidateChargeDeferredValue(string ExpectedValue)
        {
            ValidateElementValue(chargeDeferredField, ExpectedValue);

            return this;
        }

        public FinancialAssessmentChargePage ValidateManualDeferred(bool ManualDeferred)
        {
            if (ManualDeferred)
            {
                ValidateElementChecked(manualDeferredYesRadioButton);
                ValidateElementNotChecked(manualDeferredNoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(manualDeferredYesRadioButton);
                ValidateElementChecked(manualDeferredNoRadioButton);
            }

            return this;
        }

        public FinancialAssessmentChargePage ValidateRoundDown(bool RoundDown)
        {
            if (RoundDown)
            {
                ValidateElementChecked(roundDownYesRadioButton);
                ValidateElementNotChecked(roundDownNoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(roundDownYesRadioButton);
                ValidateElementChecked(roundDownNoRadioButton);
            }

            return this;
        }

        public FinancialAssessmentChargePage ValidateCancelledDateValue(string ExpectedValue)
        {
            ValidateElementValue(cancelledDateField, ExpectedValue);

            return this;
        }


    }
}
