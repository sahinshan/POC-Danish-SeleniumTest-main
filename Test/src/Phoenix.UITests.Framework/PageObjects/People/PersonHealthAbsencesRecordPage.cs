using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.Finance;
using Phoenix.UITests.Framework.WebAppAPI.Entities.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonHealthAbsencesRecordPage : CommonMethods
    {
        public PersonHealthAbsencesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By mccIframe = By.XPath("//*[@id='mcc-iframe']");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonabsence')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By closeButton = By.Id("CWCloseDrawerButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By openEndedAbsencesSection = By.Id("CWSection_OpenEndedAbsence");
        readonly By deAllocateStaffFromScheduleBookingsLabel = By.Id("CWLabelHolder_deallocatestafffromscheduledbookings");
        readonly By healthAbsenceReason_LookUpButton = By.Id("CWLookupBtn_absencereasonid");
        readonly By healthAbsencePlannedStartDateTime_TextField = By.Id("CWField_plannedstartdatetime");
        readonly By healthAbsencePlannedEndDateTime_TextField = By.Id("CWField_plannedenddateandtime");
        readonly By healthAbsenceNotifiedDateTime_TextField = By.Id("CWField_notifieddateandtime");
        readonly By dialogMessage = By.XPath("//*[@id='CWDynamicDialog']/div//section/div[2]");
        readonly By dialogCloseBtn = By.Id("CWCloseButton");
        readonly By dialogHeader = By.XPath("//div[@class='mcc-dialog__header']/h2");
        readonly By healthAbsenceNotifiedTime_TextField = By.Id("CWField_notifieddateandtime_Time");
        readonly By healthAbsencePlannedEndTime_TextField = By.Id("CWField_plannedenddateandtime_Time");
        readonly By healthAbsenceActualEndTime_TextField = By.Id("CWField_actualenddateandtime_Time");
        readonly By healthAbsenceActualStartDate_TextField = By.Id("CWField_actualstartdateandtime");
        readonly By healthAbsenceActualEndDate_TextField = By.Id("CWField_actualenddateandtime");
        readonly By openEndedeAbsence_DeAllocateStaff_YesRadioBtn = By.XPath("//input[@value='CWField_deallocatestafffromscheduledbookings_1']");
        readonly By openEndedeAbsence_DeAllocateStaff_NoRadioBtn = By.XPath("//input[@value='CWField_deallocatestafffromscheduledbookings_0']");
        readonly By healthAbsenceDurationDays_TextField = By.XPath("//input[@name='CWField_durationdays']");
        readonly By healthAbsenceDurationHours_TextField = By.XPath("//input[@name='CWField_durationhours']");
        readonly By healthAbsenceInactive_YesRadioBtn = By.XPath("//input[@id='CWField_inactive_1']");
        readonly By healthAbsenceInactive_NoRadioBtn = By.XPath("//input[@id='CWField_inactive_0']");
        readonly By healthAbsencePlannedStartDate_TimeTextField = By.Id("CWField_plannedstartdatetime_Time");
        readonly By healthAbsencePlannedEndDate_TimeTextField = By.Id("CWField_plannedenddateandtime_Time");




        public PersonHealthAbsencesRecordPage WaitForPersonHealthAbsencesRecordPageToLoad(string TaskTitle)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(pageHeader);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementTextContainsText(pageHeader, "Person Absence:\r\n" + TaskTitle);


            return this;
        }

        public PersonHealthAbsencesRecordPage WaitForPersonHealthAbsencesEditRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(pageHeader);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            return this;
        }

        public PersonHealthAbsencesRecordPage WaitForPageToLoadInDrawerMode()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(mccIframe);
            this.SwitchToIframe(mccIframe);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            return this;
        }

        public PersonHealthAbsencesRecordPage ClickHealthAbsenceReasonLookupButton()
        {
            WaitForElementToBeClickable(healthAbsenceReason_LookUpButton);
            Click(healthAbsenceReason_LookUpButton);

            return this;
        }

        public PersonHealthAbsencesRecordPage InsertHealthAbsencePlannedStartDateTime(string PlannedStartDateTime)
        {
            WaitForElement(healthAbsencePlannedStartDateTime_TextField);
            SendKeys(healthAbsencePlannedStartDateTime_TextField,PlannedStartDateTime + Keys.Tab);

            return this;
        }

        public PersonHealthAbsencesRecordPage InsertHealthAbsencePlannedStarTime(string PlannedStartTime)
        {
            WaitForElement(healthAbsencePlannedStartDate_TimeTextField);
            SendKeys(healthAbsencePlannedStartDate_TimeTextField, PlannedStartTime + Keys.Tab);

            return this;
        }

      

        public PersonHealthAbsencesRecordPage ClearHealthAbsencePlannedStartDateTime()
        {
            WaitForElement(healthAbsencePlannedStartDateTime_TextField);
            ClearText(healthAbsencePlannedStartDateTime_TextField);

            return this;
        }

        public PersonHealthAbsencesRecordPage InsertHealthAbsenceActualStartDate(string ActualStartDate)
        {
            WaitForElement(healthAbsenceActualStartDate_TextField);
            SendKeys(healthAbsenceActualStartDate_TextField, ActualStartDate + Keys.Tab);

            return this;
        }

        public PersonHealthAbsencesRecordPage InsertHealthAbsencePlannedEndDateTime(string PlannedEndDateTime)
        {
            WaitForElement(healthAbsencePlannedEndDateTime_TextField);
            SendKeys(healthAbsencePlannedEndDateTime_TextField, PlannedEndDateTime);
            SendKeysWithoutClearing(healthAbsencePlannedEndDateTime_TextField, Keys.Tab);

            return this;
        }

        public PersonHealthAbsencesRecordPage ClearHealthAbsencePlannedEndDateTime()
        {
            WaitForElement(healthAbsencePlannedEndDateTime_TextField);
            ClearText(healthAbsencePlannedEndDateTime_TextField);

            return this;
        }

        public PersonHealthAbsencesRecordPage InsertHealthAbsencePlannedEndTime(string PlannedEndTime)
        {
            WaitForElement(healthAbsencePlannedEndTime_TextField);
            Click(healthAbsencePlannedEndTime_TextField);
            ClearText(healthAbsencePlannedEndTime_TextField);
            System.Threading.Thread.Sleep(1000);
            SendKeys(healthAbsencePlannedEndTime_TextField, PlannedEndTime);
           

            return this;
        }

        public PersonHealthAbsencesRecordPage InsertHealthAbsenceActualEndTime(string PlannedEndTime)
        {
            WaitForElement(healthAbsenceActualEndTime_TextField);
            Click(healthAbsenceActualEndTime_TextField);
            ClearText(healthAbsenceActualEndTime_TextField);
            System.Threading.Thread.Sleep(1000);
            SendKeys(healthAbsenceActualEndTime_TextField, PlannedEndTime);


            return this;
        }

        public PersonHealthAbsencesRecordPage InsertHealthAbsenceActualEndDate(string ActualEndDate)
        {
            WaitForElement(healthAbsenceActualEndDate_TextField);
            SendKeys(healthAbsenceActualEndDate_TextField, ActualEndDate+Keys.Tab);

            return this;
        }

        public PersonHealthAbsencesRecordPage InsertHealthAbsenceNotifiedDateTime(string NotifiedDateTime)
        {
            WaitForElement(healthAbsenceNotifiedDateTime_TextField);
            SendKeys(healthAbsenceNotifiedDateTime_TextField, NotifiedDateTime + Keys.Tab);

            return this;
        }

        public PersonHealthAbsencesRecordPage InsertHealthAbsenceNotifiedTime(string NotifiedTime)
        {
            WaitForElement(healthAbsenceNotifiedTime_TextField);
            Click(healthAbsenceNotifiedTime_TextField);
            ClearText(healthAbsenceNotifiedTime_TextField);
            System.Threading.Thread.Sleep(1000);
            SendKeys(healthAbsenceNotifiedTime_TextField, NotifiedTime);

            return this;
        }

        public PersonHealthAbsencesRecordPage ClickHealthAbsenceSaveNClose()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            ScrollToElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public PersonHealthAbsencesRecordPage ClickHealthAbsenceSave()
        {
            WaitForElementToBeClickable(saveButton);
            ScrollToElement(saveButton);
            Click(saveButton);

            return this;
        }

        //Click Close Button
        public PersonHealthAbsencesRecordPage ClickHealthAbsenceCloseButton()
        {
            WaitForElementToBeClickable(closeButton);
            ScrollToElement(closeButton);
            Click(closeButton);

            return this;
        }

        public PersonHealthAbsencesRecordPage WaitForDialogPageToLoad()
        {
            WaitForElementVisible(dialogHeader);
            WaitForElementVisible(dialogMessage);
            WaitForElementVisible(dialogCloseBtn);
            return this;
        }

        public PersonHealthAbsencesRecordPage ValidateDialogText(string ExpectedText)
        {
            ValidateElementText(dialogMessage, ExpectedText);
            return this;
        }

        

        public PersonHealthAbsencesRecordPage ClickDialogClose()
        {
            WaitForElementToBeClickable(dialogCloseBtn);
            Click(dialogCloseBtn);
            return this;
        }

        public PersonHealthAbsencesRecordPage ClickDeleteRecordBtn()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);
            return this;
        }

        public PersonHealthAbsencesRecordPage VerifyOpenEndedAbsenceNotVisible()
        {
            WaitForElementNotVisible(openEndedAbsencesSection, 3);
            return this;
        }

        public PersonHealthAbsencesRecordPage VerifyOpenEndedAbsenceVisible()
        {
            WaitForElementVisible(openEndedAbsencesSection);
            WaitForElementVisible(deAllocateStaffFromScheduleBookingsLabel);
            return this;
        }

        public PersonHealthAbsencesRecordPage ValidateDeAllocateStaff_NoRadioButtonChecked()
        {
            WaitForElement(openEndedeAbsence_DeAllocateStaff_NoRadioBtn);
            ValidateElementChecked(openEndedeAbsence_DeAllocateStaff_NoRadioBtn);

            return this;
        }

        public PersonHealthAbsencesRecordPage ClickDeAllocateStaff_YesRadioButton()
        {
            WaitForElementToBeClickable(openEndedeAbsence_DeAllocateStaff_YesRadioBtn);
            Click(openEndedeAbsence_DeAllocateStaff_YesRadioBtn);

            return this;
        }

        
        public PersonHealthAbsencesRecordPage ValidateDeleteRecordButton_NotPresent(bool ExpectedNotVisible)
        {
            if (ExpectedNotVisible)
            {
                WaitForElementNotVisible(deleteButton, 5);
            }
            else
            {
                WaitForElementVisible(deleteButton);
            }

            return this;
        }

        public PersonHealthAbsencesRecordPage ValidateDurationHoursText(string ExpectedText)
        {
            ValidateElementValue(healthAbsenceDurationHours_TextField, ExpectedText);
            return this;
        }

        public PersonHealthAbsencesRecordPage ValidateDurationDaysText(string ExpectedText)
        {
            ValidateElementValue(healthAbsenceDurationDays_TextField, ExpectedText);
            return this;
        }

        public PersonHealthAbsencesRecordPage ClickHealthAbsenceInactiveYesRadio()
        {
            ScrollToElement(healthAbsenceInactive_YesRadioBtn);
            WaitForElementToBeClickable(healthAbsenceInactive_YesRadioBtn);
            Click(healthAbsenceInactive_YesRadioBtn);

            return this;
        }

        public PersonHealthAbsencesRecordPage ClickHealthAbsenceInactiveNoRadio()
        {
            WaitForElementToBeClickable(healthAbsenceInactive_NoRadioBtn);
            Click(healthAbsenceInactive_NoRadioBtn);

            return this;
        }
    }
}
