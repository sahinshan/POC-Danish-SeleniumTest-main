using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ScheduleSetupRecordPage : CommonMethods
    {

        public ScheduleSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By scheduleSetupIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=schedulesetup&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        

        
        readonly By chargingRuleLookupButton = By.Id("CWLookupBtn_chargingruletypeid");
        readonly By scheduleTypeLookupButton = By.Id("CWLookupBtn_scheduletypeid");

        readonly By startDateField = By.Id("CWField_startdate");
        readonly By endDateField = By.Id("CWField_enddate");

        readonly By roundDownNoOptionField = By.Id("CWField_rounddowncharge_0");
        readonly By roundDownYesOptionField = By.Id("CWField_rounddowncharge_1");

        readonly By adjustedDaysField = By.Id("CWField_adjusteddays");


        readonly By calculateSavingsCreditNoOptionField = By.Id("CWField_calculatesavingscredit_0");
        readonly By calculateSavingsCreditYesOptionField = By.Id("CWField_calculatesavingscredit_1");





        public ScheduleSetupRecordPage WaitForScheduleSetupRecordPageToLoad(string ScheduleSetupName)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(scheduleSetupIFrame);
            this.SwitchToIframe(scheduleSetupIFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(endDateField);
            this.WaitForElement(roundDownNoOptionField);
            this.WaitForElement(roundDownYesOptionField);
            this.WaitForElement(calculateSavingsCreditNoOptionField);
            this.WaitForElement(calculateSavingsCreditYesOptionField);
            this.WaitForElement(adjustedDaysField);

            if (driver.FindElement(pageHeader).Text != "Schedule Setup: " + ScheduleSetupName)
                throw new Exception("Page title do not equals: Schedule Setup: " + ScheduleSetupName);

            return this;
        }

        public ScheduleSetupRecordPage ClickChargingRuleLookupButton()
        {
            this.Click(chargingRuleLookupButton);

            return this;
        }

        public ScheduleSetupRecordPage ClickScheduleTypeLookupButton()
        {
            this.Click(scheduleTypeLookupButton);

            return this;
        }

        public ScheduleSetupRecordPage InsertStartDate(string StartDate)
        {
            this.SendKeys(startDateField, StartDate);

            return this;
        }

        public ScheduleSetupRecordPage InsertEndDate(string EndDate)
        {
            this.SendKeys(endDateField, EndDate);

            return this;
        }

        public ScheduleSetupRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ScheduleSetupRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            return this;
        }

        public ScheduleSetupRecordPage ClickSaveAndCloseButtonAndWaitForRefreshPanelToClose()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ScheduleSetupRecordPage SelectRoundDownCharges(bool RoundDownCharges)
        {
            if(RoundDownCharges)
                this.Click(roundDownYesOptionField);
            else
                this.Click(roundDownNoOptionField);

            return this;
        }

        public ScheduleSetupRecordPage SelectCalculateSavingsCredit(bool CalculateSavingsCredit)
        {
            if (CalculateSavingsCredit)
                this.Click(calculateSavingsCreditYesOptionField);
            else
                this.Click(calculateSavingsCreditNoOptionField);

            return this;
        }

        public ScheduleSetupRecordPage InsertAdjustedDays(string AdjustedDays)
        {
            this.SendKeys(adjustedDaysField, AdjustedDays);

            return this;
        }



    }
}
