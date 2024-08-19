
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.Cases.Health;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// Person Record - Care Plans Tab - Regular Care Tasks Sub Tab
    /// </summary>
    public class PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage : CommonMethods
    {
        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpregularcaretaskschedule&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By mccIframe = By.Id("mcc-iframe");

        readonly By CWSubTabsPanel_IFrame = By.Id("CWUrlPanel_IFrame");
        readonly By _Person_LookupButton = By.Id("CWLookupBtn_personid");
        readonly By _RegularCare_LookupButton = By.Id("CWLookupBtn_regularcaretaskid");
        readonly By _RegularCare_StartDateField = By.Id("CWField_startdate");
        readonly By _RegularCare_EndDateField = By.Id("CWField_enddate");
        readonly By _RegularCare_SelectTimeOrShiftField = By.Id("CWField_timeorshiftid");
        readonly By _RegularCare_SelectTimeLabel = By.XPath("//li[@id='CWLabelHolder_selecttimeforcaretobegiven']/label");
        readonly By _RegularCare_SelectShiftLabel = By.XPath("//li[@id='CWLabelHolder_selectshiftforcaretobegivenid']/label");
        readonly By _RegularCare_SelectTimeLabelField = By.XPath("//input[@id='CWField_selecttimeforcaretobegiven']");
        readonly By _RegularCare_SelectShiftLookUpBtn = By.XPath("//button[@id='CWLookupBtn_selectshiftforcaretobegivenid']");
        readonly By _RegularCare_LastRunDateField = By.XPath("//input[@id='CWField_lastrundate']");
        readonly By _RegularCare_RecurrencePatternPickList = By.XPath("//*[@id='CWField_recurrencepatternid']");
        readonly By _RegularCare_RecurEveryXHourLabel = By.XPath("//li[@id='CWLabelHolder_recureveryxhour']/label");
        readonly By _RegularCare_RecurEveryXHourField = By.XPath("//input[@id='CWField_recureveryxhour']");
        readonly By _RegularCare_DoesNotOccurFromLabel = By.XPath("//li[@id='CWLabelHolder_notbetweenfrom']/label");
        readonly By _RegularCare_DoesNotOccurFromField = By.XPath("//input[@id='CWField_notbetweenfrom']");
        readonly By _RegularCare_DoesNotOccurToLabel = By.XPath("//li[@id='CWLabelHolder_notbetweento']/label");
        readonly By _RegularCare_DoesNotOccurToField = By.XPath("//input[@id='CWField_notbetweento']");
        readonly By _RegularCare_RecurEveryXDayLabel = By.XPath("//li[@id='CWLabelHolder_recureveryxday']/label");
        readonly By _RegularCare_RecurEveryXDayField = By.XPath("//input[@id='CWField_recureveryxday']");
        readonly By _RegularCare_RecurEveryXWeekLabel = By.XPath("//li[@id='CWLabelHolder_recureveryxweek']/label");
        readonly By _RegularCare_RecurEveryXWeekField = By.XPath("//input[@id='CWField_recureveryxweek']");
        readonly By _RegularCare_SaveBtn = By.Id("TI_SaveButton");
        readonly By _RegularCare_SaveNCloseBtn = By.Id("TI_SaveAndCloseButton");
        readonly By _RegularCare_DeleteBtn = By.Id("TI_DeleteRecordButton");
        readonly By Inactive_No = By.XPath("//*[@id='CWField_inactive_0']");
        readonly By Inactive_Yes = By.XPath("//*[@id='CWField_inactive_1']");
        readonly By _RegularCare_CloseBtn = By.Id("CWCloseDrawerButton");


        By _RegularCare_RecurEveryXWeekDaysLabel(string dayoftheWeek) => By.XPath("//li[@id='CWLabelHolder_" + dayoftheWeek + "']");
        By _RegularCare_SelectRecurEveryXWeekDays(string dayoftheWeek, int option) => By.XPath("//input[@id='CWField_" + dayoftheWeek + "_" + option + "']");



        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage WaitForPersonCarePlansSubPage_RegularCareTasks_CareScheludesRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage WaitForCareScheludesRecordPageToLoadinDrawerMode()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(mccIframe);
            SwitchToIframe(mccIframe);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidatePersonLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                Assert.IsTrue(CheckIfElementExists(_Person_LookupButton));
            }
            else
            {
                Assert.IsFalse(CheckIfElementExists(_Person_LookupButton));
            }

            return this;
        }


        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateRegularCareLookupButtonVisible(bool ExpectButtonVisible)
        {
            System.Threading.Thread.Sleep(1000);
            if (ExpectButtonVisible)
            {
                WaitForElementVisible(_RegularCare_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(_RegularCare_LookupButton, 5);
            }

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ClickRegularCareLookupButton()
        {
            WaitForElementToBeClickable(_RegularCare_LookupButton);
            Click(_RegularCare_LookupButton);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateStartDateFieldValue(string ExpectedText)
        {
            ValidateElementValueByJavascript("CWField_startdate", ExpectedText);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateEndDateFieldValue(string ExpectedText)
        {
            ValidateElementValueByJavascript("CWField_enddate", ExpectedText);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage SelectTimeRShift(string TextToSelect)
        {
            SelectPicklistElementByText(_RegularCare_SelectTimeOrShiftField, TextToSelect);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateSelectTimeLabel(string ExpectText)
        {
            ValidateElementText(_RegularCare_SelectTimeLabel, ExpectText);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateTimeLableFieldVisibility(bool Visibility)
        {
            if (Visibility)
            {
                WaitForElementVisible(_RegularCare_SelectTimeLabelField);
            }
            else
            {
                WaitForElementNotVisible(_RegularCare_SelectTimeLabelField, 3);
            }

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage SetStartDateFieldValue(string ExpectedText)
        {
            WaitForElement(_RegularCare_StartDateField);
            SendKeys(_RegularCare_StartDateField, ExpectedText + Keys.Tab);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage SetEndDateFieldValue(string ExpectedText)
        {
            WaitForElement(_RegularCare_EndDateField);
            SendKeys(_RegularCare_EndDateField, ExpectedText + Keys.Tab);
            return this;
        }


        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage SetSelectTimeField(string TimeText)
        {
            WaitForElement(_RegularCare_SelectTimeLabelField);
            SendKeys(_RegularCare_SelectTimeLabelField, TimeText + Keys.Tab);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateSelectShiftLabel(string ExpectText)
        {
            ValidateElementText(_RegularCare_SelectShiftLabel, ExpectText);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ClickSelectShiftLookUp()
        {
            WaitForElementToBeClickable(_RegularCare_SelectShiftLookUpBtn);
            Click(_RegularCare_SelectShiftLookUpBtn);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateLastRunDateFieldVisibility(bool Visibility)
        {
            if (Visibility)
            {
                WaitForElementVisible(_RegularCare_LastRunDateField);
            }
            else
            {
                WaitForElementNotVisible(_RegularCare_LastRunDateField, 3);
            }

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateRecurrencePatternPicklist_FieldOptionIsPresent(string OptionName)
        {
            WaitForElementVisible(_RegularCare_RecurrencePatternPickList);
            ValidatePicklistContainsElementByText(_RegularCare_RecurrencePatternPickList, OptionName);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateRecurrencePatternPicklistSelectedText(string ExpectedValue)
        {
            ScrollToElement(_RegularCare_RecurrencePatternPickList);
            WaitForElementVisible(_RegularCare_RecurrencePatternPickList);
            ValidatePicklistSelectedText(_RegularCare_RecurrencePatternPickList, ExpectedValue);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage SelectRecurrencePatternPicklist(string TextToSelect)
        {
            WaitForElementVisible(_RegularCare_RecurrencePatternPickList);
            SelectPicklistElementByText(_RegularCare_RecurrencePatternPickList, TextToSelect);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateRecurEveryXHourLabel(string ExpectText)
        {
            ValidateElementText(_RegularCare_RecurEveryXHourLabel, ExpectText);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage SetRecurEveryXHourField(string RecurEveryXHour)
        {
            WaitForElementVisible(_RegularCare_RecurEveryXHourField);
            SendKeys(_RegularCare_RecurEveryXHourField, RecurEveryXHour);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateDoesNotOccurFromLabel(bool Visibility, string ExpectText)
        {
            if (Visibility)
            {
                WaitForElementVisible(_RegularCare_DoesNotOccurFromField);
                ValidateElementText(_RegularCare_DoesNotOccurFromLabel, ExpectText);

            }
            else
            {
                WaitForElementNotVisible(_RegularCare_DoesNotOccurFromField, 3);
            }
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateDoesNotOccurToLabel(bool Visibility, string ExpectText)
        {
            if (Visibility)
            {
                WaitForElementVisible(_RegularCare_DoesNotOccurToField);
                ValidateElementText(_RegularCare_DoesNotOccurToLabel, ExpectText);

            }
            else
            {
                WaitForElementNotVisible(_RegularCare_DoesNotOccurToField, 3);
            }
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateRecurEveryXDayLabel(bool Visibility, string ExpectText)
        {
            if (Visibility)
            {
                WaitForElementVisible(_RegularCare_RecurEveryXDayField);
                ValidateElementText(_RegularCare_RecurEveryXDayLabel, ExpectText);

            }
            else
            {
                WaitForElementNotVisible(_RegularCare_RecurEveryXDayField, 3);
            }
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage SetRecurEveryXDay(string RecurEveryXDay)
        {

            WaitForElementVisible(_RegularCare_RecurEveryXDayField);
            SendKeys(_RegularCare_RecurEveryXDayField, RecurEveryXDay);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateRecurEveryXWeekLabel(bool Visibility, string ExpectText)
        {
            if (Visibility)
            {
                WaitForElementVisible(_RegularCare_RecurEveryXWeekField);
                ValidateElementText(_RegularCare_RecurEveryXWeekLabel, ExpectText);

            }
            else
            {
                WaitForElementNotVisible(_RegularCare_RecurEveryXWeekField, 3);
            }
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage SetRecurEveryXWeek(string RecurEveryXWeek)
        {

            WaitForElementVisible(_RegularCare_RecurEveryXWeekField);
            SendKeys(_RegularCare_RecurEveryXWeekField, RecurEveryXWeek);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateRadioButtonRecurEveryXDayTextVisibility(string dayOfTheWeek, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(_RegularCare_RecurEveryXWeekDaysLabel(dayOfTheWeek));
            else
                WaitForElementNotVisible(_RegularCare_RecurEveryXWeekDaysLabel(dayOfTheWeek), 7);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateRadioButtonRecurEveryXDayOptionVisibility(string dayOfTheWeek, int position, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(_RegularCare_SelectRecurEveryXWeekDays(dayOfTheWeek, position));
            else
                WaitForElementNotVisible(_RegularCare_SelectRecurEveryXWeekDays(dayOfTheWeek, position), 7);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage clickSaveBtn()
        {
            WaitForElementToBeClickable(_RegularCare_SaveBtn);
            Click(_RegularCare_SaveBtn);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage clickSaveNCloseBtn()
        {
            WaitForElementToBeClickable(_RegularCare_SaveNCloseBtn);
            Click(_RegularCare_SaveNCloseBtn);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage clickCloseDrawerBtn()
        {
            WaitForElementToBeClickable(_RegularCare_CloseBtn);
            Click(_RegularCare_CloseBtn);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateDeleteButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                WaitForElementVisible(_RegularCare_DeleteBtn);
                Assert.IsTrue(CheckIfElementExists(_RegularCare_DeleteBtn));
            }
            else
            {
                WaitForElementNotVisible(_RegularCare_DeleteBtn, 3);
            }

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ClickInactive_YesRadioButton()
        {
            WaitForElement(Inactive_Yes);
            Click(Inactive_Yes);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage ValidateLastRunDate(string ExpectedText)
        {

            ValidateElementValueByJavascript("CWField_lastrundate", ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTask_CareScheludesRecordPage SelectRadioButtonRecurEveryXDayOption(string dayOfTheWeek, int position)
        {

            Click(_RegularCare_SelectRecurEveryXWeekDays(dayOfTheWeek, position));
            return this;
        }

    }
}
