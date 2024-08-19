using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserUserWorkScheduleRecordPage : CommonMethods
    {
        public SystemUserUserWorkScheduleRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By workScheduleIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=userworkschedule&')]");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");

        readonly By Title_Field = By.Id("CWField_title");
        readonly By StartDate_Field = By.Id("CWField_startdate");
        readonly By StartTime_Field = By.Id("CWField_starttime");
        readonly By EndTime_Field = By.Id("CWField_endtime");
        
        readonly By recurrencePatter_LookupButton = By.Id("CWLookupBtn_recurrencepatternid");


        public SystemUserUserWorkScheduleRecordPage WaitForSystemUserUserWorkScheduleRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(workScheduleIFrame);
            SwitchToIframe(workScheduleIFrame);

            WaitForElement(Title_Field);
            WaitForElement(StartDate_Field);

            return this;
        }

        public SystemUserUserWorkScheduleRecordPage InsertTextTitleTextBar(String ValueToInsert)
        {
            WaitForElement(Title_Field);
            SendKeys(Title_Field, ValueToInsert);

            return this;
        }
        public SystemUserUserWorkScheduleRecordPage InsertStartDate(String ValueToInsert)
        {
            WaitForElement(StartDate_Field);
            SendKeys(StartDate_Field, ValueToInsert);

            return this;
        }
        public SystemUserUserWorkScheduleRecordPage InsertStartTime(String ValueToInsert)
        {
            WaitForElement(StartTime_Field);
            SendKeys(StartTime_Field, ValueToInsert);

            return this;
        }
        public SystemUserUserWorkScheduleRecordPage InsertEndTime(String ValueToInsert)
        {
            WaitForElement(EndTime_Field);
            SendKeys(EndTime_Field, ValueToInsert);

            return this;
        }
        public SystemUserUserWorkScheduleRecordPage ClickRecurrencePatterLookupButton()
        {
            WaitForElement(EndTime_Field);
            Click(recurrencePatter_LookupButton);

            return this;
        }
        public SystemUserUserWorkScheduleRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public SystemUserUserWorkScheduleRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public SystemUserUserWorkScheduleRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;
        }
    }
}
