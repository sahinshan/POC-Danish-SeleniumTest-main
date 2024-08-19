using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class AvailabilityTypeRecordPage : CommonMethods
    {
        public AvailabilityTypeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContent_Iframe = By.Id("CWContentIFrame");        
        readonly By availabilityTypes_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=availabilitytypes&')]");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");

        readonly By availabilityName_Field = By.Id("CWField_name");
        readonly By availabilityName_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");

        readonly By precedence_Field = By.Id("CWField_precedence");
        readonly By precedence_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_precedence']/label/span");

        readonly By fixedWorkingPattern_YesOption = By.Id("CWField_isfixedworkingpattern_1");
        readonly By fixedWorkingPattern_NoOption = By.Id("CWField_isfixedworkingpattern_0");

        readonly By responsibleTeam_LookUp = By.Id("CWLookupBtn_ownerid");
        readonly By responsibleTeam_FieldLinkText = By.Id("CWField_ownerid_Link");

        readonly By ValidForExport_YesRadioButtonOption = By.Id("CWField_validforexport_1");
        readonly By ValidForExport_NoRadioButtonOption = By.Id("CWField_validforexport_0");

        readonly By DiaryBookings_Picklist = By.Id("CWField_diarybookingsvalidityid");
        readonly By ScheduleBookings_Picklist = By.Id("CWField_regularbookingsvalidityid");

        readonly By CountTowardsHoursOrDaysWorked_YesRadioButtonOption = By.XPath("//*[@id='CWField_countstowardsperiodworked_1']");
        readonly By CountTowardsHoursOrDaysWorked_NoRadioButtonOption = By.XPath("//*[@id='CWField_countstowardsperiodworked_0']");

        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");

        public AvailabilityTypeRecordPage WaitForAvailabilityTypeRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(availabilityTypes_Iframe);
            SwitchToIframe(availabilityTypes_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(pageHeader);

            WaitForElement(availabilityName_Field);

            return this;
        }


        public AvailabilityTypeRecordPage InsertName(string TextToInsert)
        {
            ScrollToElement(availabilityName_Field);
            WaitForElementToBeClickable(availabilityName_Field);
            SendKeys(availabilityName_Field, TextToInsert);
            SendKeysWithoutClearing(availabilityName_Field, Keys.Tab);

            return this;
        }

        public AvailabilityTypeRecordPage InsertPrecedence(string TextToInsert)
        {
            ScrollToElement(precedence_Field);
            WaitForElementToBeClickable(precedence_Field);
            SendKeys(precedence_Field, TextToInsert);
            SendKeysWithoutClearing(precedence_Field, Keys.Tab);

            return this;
        }

        public AvailabilityTypeRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(responsibleTeam_LookUp);
            ScrollToElement(responsibleTeam_LookUp);
            Click(responsibleTeam_LookUp);
            return this;
        }

        public AvailabilityTypeRecordPage SelectValueForDiaryBookings(string TextToSelect)
        {
            WaitForElementToBeClickable(DiaryBookings_Picklist);
            ScrollToElement(DiaryBookings_Picklist);
            SelectPicklistElementByText(DiaryBookings_Picklist, TextToSelect);

            return this;
        }

        public AvailabilityTypeRecordPage SelectValueForScheduleBookings(string TextToSelect)
        {            
            WaitForElementToBeClickable(ScheduleBookings_Picklist);
            ScrollToElement(ScheduleBookings_Picklist);
            SelectPicklistElementByText(ScheduleBookings_Picklist, TextToSelect);

            return this;
        }

        public AvailabilityTypeRecordPage ClickIsAvailabilityTypeAFixedWorkingPatternYesRadioButton()
        {            
            WaitForElementToBeClickable(fixedWorkingPattern_YesOption);
            ScrollToElement(fixedWorkingPattern_YesOption);
            Click(fixedWorkingPattern_YesOption);

            return this;
        }

        public AvailabilityTypeRecordPage ClickIsAvailabilityTypeAFixedWorkingPatternNoRadioButton()
        {            
            WaitForElementToBeClickable(fixedWorkingPattern_NoOption);
            ScrollToElement(fixedWorkingPattern_NoOption);
            Click(fixedWorkingPattern_NoOption);

            return this;
        }


        public AvailabilityTypeRecordPage ClickCountTowardsHoursOrDaysWorkedYesRadioButton()
        {
            ScrollToElement(CountTowardsHoursOrDaysWorked_YesRadioButtonOption);
            WaitForElementToBeClickable(CountTowardsHoursOrDaysWorked_YesRadioButtonOption);
            Click(CountTowardsHoursOrDaysWorked_YesRadioButtonOption);

            return this;
        }

        public AvailabilityTypeRecordPage ClickCountTowardsHoursOrDaysWorkedNoRadioButton()
        {
            ScrollToElement(CountTowardsHoursOrDaysWorked_NoRadioButtonOption);
            WaitForElementToBeClickable(CountTowardsHoursOrDaysWorked_NoRadioButtonOption);
            Click(CountTowardsHoursOrDaysWorked_NoRadioButtonOption);

            return this;
        }

        public AvailabilityTypeRecordPage ClickSaveAndCloseButton()
        {            
            WaitForElementToBeClickable(saveAndCloseButton);
            ScrollToElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public AvailabilityTypeRecordPage ClickSaveButton()
        {            
            WaitForElementToBeClickable(saveButton);
            ScrollToElement(saveButton);
            Click(saveButton);

            return this;
        }

        public AvailabilityTypeRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);            
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public AvailabilityTypeRecordPage ClickDeleteButton()
        {
            ScrollToElement(deleteButton);
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public AvailabilityTypeRecordPage ClickBackButton()
        {
            ScrollToElement(backButton);
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public AvailabilityTypeRecordPage ValidateNotificationMessageText(String ExpectedText)
        {
            WaitForElementVisible(NotificationMessage);
            ScrollToElement(NotificationMessage);
            ValidateElementTextContainsText(NotificationMessage, ExpectedText);
            return this;
        }

        public AvailabilityTypeRecordPage ValidateAvailabilityTypeNameFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(availabilityName_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(availabilityName_FieldErrorLabel, 3);
            }

            return this;
        }

        public AvailabilityTypeRecordPage ValidatePrecedenceFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(precedence_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(precedence_FieldErrorLabel, 3);
            }

            return this;
        }



        public AvailabilityTypeRecordPage ValidateAvailabilityTypeNameFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(availabilityName_FieldErrorLabel);
            ScrollToElement(availabilityName_FieldErrorLabel);
            ValidateElementByTitle(availabilityName_FieldErrorLabel, ExpectedText);

            return this;
        }

        public AvailabilityTypeRecordPage ValidatePrecedenceFieldErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(precedence_FieldErrorLabel);
            ScrollToElement(precedence_FieldErrorLabel);
            ValidateElementByTitle(precedence_FieldErrorLabel, ExpectedText);

            return this;
        }

        public AvailabilityTypeRecordPage ValidatePrecedenceFieldMaximumLengthValue(string ExpectedLengthValue)
        {
            WaitForElementVisible(precedence_Field);
            ScrollToElement(precedence_Field);
            ValidateElementMaxLength(precedence_Field, ExpectedLengthValue);
            return this;
        }

        public AvailabilityTypeRecordPage ValidateAvailabilityNameFieldValue(string ExpectedText)
        {
            ScrollToElement(availabilityName_Field);
            WaitForElementVisible(availabilityName_Field);
            ValidateElementValue(availabilityName_Field, ExpectedText);

            return this;
        }

        public AvailabilityTypeRecordPage ValidatePrecedenceFieldValue(string ExpectedText)
        {
            ScrollToElement(precedence_Field);
            WaitForElementVisible(precedence_Field);
            ValidateElementValue(precedence_Field, ExpectedText);

            return this;
        }

        public AvailabilityTypeRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(responsibleTeam_FieldLinkText);
            ValidateElementByTitle(responsibleTeam_FieldLinkText, ExpectedText);

            return this;
        }

        public AvailabilityTypeRecordPage ValidateValidForExportNoRadioButtonChecked()
        {
            ValidateElementChecked(ValidForExport_NoRadioButtonOption);
            ValidateElementNotChecked(ValidForExport_YesRadioButtonOption);

            return this;
        }

        public AvailabilityTypeRecordPage ValidateValidForExportYesRadioButtonChecked()
        {
            ValidateElementChecked(ValidForExport_YesRadioButtonOption);
            ValidateElementNotChecked(ValidForExport_NoRadioButtonOption);

            return this;
        }

        public AvailabilityTypeRecordPage ValidateIsThisAvailabilityTypeAFixedWorkingPatternYesRadionButtonChecked()
        {           
            ValidateElementChecked(fixedWorkingPattern_YesOption);
            ValidateElementNotChecked(fixedWorkingPattern_NoOption);
  
            return this;
        }

        public AvailabilityTypeRecordPage ValidateIsThisAvailabilityTypeAFixedWorkingPatternNoRadionButtonChecked()
        {
            ValidateElementChecked(fixedWorkingPattern_NoOption);
            ValidateElementNotChecked(fixedWorkingPattern_YesOption);

            return this;
        }

        public AvailabilityTypeRecordPage ValidateValueForDiaryBookingsSelectedText(string ExpectedText)
        {
            ScrollToElement(DiaryBookings_Picklist);
            WaitForElementVisible(DiaryBookings_Picklist);
            ValidatePicklistSelectedText(DiaryBookings_Picklist, ExpectedText);

            return this;
        }

        public AvailabilityTypeRecordPage ValidateValueForScheduleBookingsSelectedText(string ExpectedText)
        {
            ScrollToElement(ScheduleBookings_Picklist);
            WaitForElementVisible(ScheduleBookings_Picklist);
            ValidatePicklistSelectedText(ScheduleBookings_Picklist, ExpectedText);

            return this;
        }

        public AvailabilityTypeRecordPage ValidateCountsTowardsHoursOrDaysWorkedYesRadionButtonChecked()
        {
            ValidateElementChecked(CountTowardsHoursOrDaysWorked_YesRadioButtonOption);
            ValidateElementNotChecked(CountTowardsHoursOrDaysWorked_NoRadioButtonOption);

            return this;
        }
        public AvailabilityTypeRecordPage ValidateCountsTowardsHoursOrDaysWorkedNoRadionButtonChecked()
        {
            ValidateElementChecked(CountTowardsHoursOrDaysWorked_NoRadioButtonOption);
            ValidateElementNotChecked(CountTowardsHoursOrDaysWorked_YesRadioButtonOption);

            return this;
        }

    }
}
