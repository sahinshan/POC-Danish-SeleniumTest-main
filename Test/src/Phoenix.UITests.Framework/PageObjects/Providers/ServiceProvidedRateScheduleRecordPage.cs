using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ServiceProvidedRateScheduleRecordPage : CommonMethods
    {
        public ServiceProvidedRateScheduleRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");        
        readonly By ServiceProvidedRateScheduleRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovidedrateschedule&')]");
        readonly By pageHeader = By.XPath("//h1");
        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By DeleteButton = By.XPath("//button[@title = 'Delete']");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By ServiceProvidedRatePeriodLink = By.XPath("//*[@id='CWField_serviceprovidedrateperiodid_Link']");
        readonly By ServiceProvidedRatePeriodLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceprovidedrateperiodid']");
        readonly By Rate = By.XPath("//*[@id='CWField_rate']");
        readonly By RateBankHolidayField = By.Id("CWField_ratebankholiday");
        readonly By TimeBandStartField = By.Id("CWField_timebandstart");
        readonly By TimeBandEndField = By.Id("CWField_timebandend");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ServiceProvidedLink = By.XPath("//*[@id='CWField_serviceprovidedid_Link']");
        readonly By ServiceProvidedLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceprovidedid']");
        readonly By Selectall_YesOption = By.XPath("//*[@id='CWField_selectall_1']");
        readonly By Selectall_NoOption = By.XPath("//*[@id='CWField_selectall_0']");
        readonly By Monday_YesOption = By.XPath("//*[@id='CWField_monday_1']");
        readonly By Monday_NoOption = By.XPath("//*[@id='CWField_monday_0']");
        readonly By Tuesday_YesOption = By.XPath("//*[@id='CWField_tuesday_1']");
        readonly By Tuesday_NoOption = By.XPath("//*[@id='CWField_tuesday_0']");
        readonly By Wednesday_YesOption = By.XPath("//*[@id='CWField_wednesday_1']");
        readonly By Wednesday_NoOption = By.XPath("//*[@id='CWField_wednesday_0']");
        readonly By Thursday_YesOption = By.XPath("//*[@id='CWField_thursday_1']");
        readonly By Thursday_NoOption = By.XPath("//*[@id='CWField_thursday_0']");
        readonly By Friday_YesOption = By.XPath("//*[@id='CWField_friday_1']");
        readonly By Friday_NoOption = By.XPath("//*[@id='CWField_friday_0']");
        readonly By Saturday_YesOption = By.XPath("//*[@id='CWField_saturday_1']");
        readonly By Saturday_NoOption = By.XPath("//*[@id='CWField_saturday_0']");
        readonly By Sunday_YesOption = By.XPath("//*[@id='CWField_sunday_1']");
        readonly By Sunday_NoOption = By.XPath("//*[@id='CWField_sunday_0']");
        readonly By NotesTextArea = By.XPath("//*[@id='CWField_notes']");
        readonly By TimeBandStartFieldError = By.XPath("//*[@id = 'CWControlHolder_timebandstart']//label[@class = 'formerror']/span");
        readonly By TimeBandEndFieldError = By.XPath("//*[@id = 'CWControlHolder_timebandend']//label[@class = 'formerror']/span");


        public ServiceProvidedRateScheduleRecordPage WaitForServiceProvidedRateScheduleRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ServiceProvidedRateScheduleRecordIFrame);
            SwitchToIframe(ServiceProvidedRateScheduleRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElement(pageHeader);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            MoveToElementInPage(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            MoveToElementInPage(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteButton);
            MoveToElementInPage(DeleteButton);
            Click(DeleteButton);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 50);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickServiceProvidedRatePeriodidLink()
        {
            WaitForElementToBeClickable(ServiceProvidedRatePeriodLink);
            Click(ServiceProvidedRatePeriodLink);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateServiceProvidedRatePeriodLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ServiceProvidedRatePeriodLink);
            ValidateElementText(ServiceProvidedRatePeriodLink, ExpectedText);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickServiceProvidedRatePeriodLookupButton()
        {
            WaitForElementToBeClickable(ServiceProvidedRatePeriodLookupButton);
            Click(ServiceProvidedRatePeriodLookupButton);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateServiceProvidedRatePeriodLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(ServiceProvidedRatePeriodLookupButton);
            else
                ValidateElementNotDisabled(ServiceProvidedRatePeriodLookupButton);
            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateRateText(string ExpectedText)
        {
            WaitForElementVisible(Rate);
            ValidateElementValue(Rate, ExpectedText);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateRateFieldDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(Rate);
            else
                ValidateElementNotDisabled(Rate);
            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateRateBankHolidayText(string ExpectedText)
        {
            WaitForElementVisible(RateBankHolidayField);
            ValidateElementValue(RateBankHolidayField, ExpectedText);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateRateBankHolidayFieldDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(RateBankHolidayField);
            else
                ValidateElementNotDisabled(RateBankHolidayField);
            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTimeBandStartText(string ExpectedText)
        {
            WaitForElementVisible(TimeBandStartField);
            ValidateElementValue(TimeBandStartField, ExpectedText);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTimeBandStartFieldDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(TimeBandEndField);
            else
                ValidateElementNotDisabled(TimeBandEndField);
            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTimeBandEndText(string ExpectedText)
        {
            WaitForElementVisible(TimeBandEndField);
            ValidateElementValue(TimeBandEndField, ExpectedText);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTimeBandEndFieldDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(TimeBandEndField);
            else
                ValidateElementNotDisabled(TimeBandEndField);
            return this;
        }

        public ServiceProvidedRateScheduleRecordPage InsertRate(string TextToInsert)
        {
            WaitForElement(Rate);
            MoveToElementInPage(Rate);
            SendKeys(Rate, TextToInsert);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage InsertRateBankHoliday(string TextToInsert)
        {
            WaitForElement(RateBankHolidayField);
            MoveToElementInPage(RateBankHolidayField);
            SendKeys(RateBankHolidayField, TextToInsert);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage InsertTimeBandStart(string TextToInsert)
        {
            WaitForElement(TimeBandStartField);
            MoveToElementInPage(TimeBandStartField);
            SendKeys(TimeBandStartField, TextToInsert);
            SendKeysWithoutClearing(TimeBandStartField, Keys.Tab);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage InsertTimeBandEnd(string TextToInsert)
        {
            WaitForElement(TimeBandEndField);
            MoveToElementInPage(TimeBandEndField);
            SendKeys(TimeBandEndField, TextToInsert);
            SendKeysWithoutClearing(TimeBandEndField, Keys.Tab);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickResponsibleTeamClearButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamClearButton);
            Click(ResponsibleTeamClearButton);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateResponsibleTeamLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(ResponsibleTeamLookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeamLookupButton);
            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickServiceProvidedLink()
        {
            WaitForElementToBeClickable(ServiceProvidedLink);
            Click(ServiceProvidedLink);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateServiceProvidedidLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ServiceProvidedLink);
            ValidateElementText(ServiceProvidedLink, ExpectedText);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickServiceProvidedLookupButton()
        {
            WaitForElementToBeClickable(ServiceProvidedLookupButton);
            Click(ServiceProvidedLookupButton);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateServiceProvidedLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(ServiceProvidedLookupButton);
            else
                ValidateElementNotDisabled(ServiceProvidedLookupButton);
            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickSelectall_YesOption()
        {
            WaitForElementToBeClickable(Selectall_YesOption);
            Click(Selectall_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSelectall_YesOptionChecked()
        {
            WaitForElement(Selectall_YesOption);
            ValidateElementChecked(Selectall_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSelectall_YesOptionNotChecked()
        {
            WaitForElement(Selectall_YesOption);
            ValidateElementNotChecked(Selectall_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickSelectall_NoOption()
        {
            WaitForElementToBeClickable(Selectall_NoOption);
            Click(Selectall_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSelectall_NoOptionChecked()
        {
            WaitForElement(Selectall_NoOption);
            ValidateElementChecked(Selectall_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSelectall_NoOptionNotChecked()
        {
            WaitForElement(Selectall_NoOption);
            ValidateElementNotChecked(Selectall_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSelectAllOptionsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Selectall_YesOption);
                ValidateElementDisabled(Selectall_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(Selectall_YesOption);
                ValidateElementNotDisabled(Selectall_NoOption);
            }
            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickMonday_YesOption()
        {
            WaitForElementToBeClickable(Monday_YesOption);
            Click(Monday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateMonday_YesOptionChecked()
        {
            WaitForElement(Monday_YesOption);
            ValidateElementChecked(Monday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateMonday_YesOptionNotChecked()
        {
            WaitForElement(Monday_YesOption);
            ValidateElementNotChecked(Monday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickMonday_NoOption()
        {
            WaitForElementToBeClickable(Monday_NoOption);
            Click(Monday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateMonday_NoOptionChecked()
        {
            WaitForElement(Monday_NoOption);
            ValidateElementChecked(Monday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateMonday_NoOptionNotChecked()
        {
            WaitForElement(Monday_NoOption);
            ValidateElementNotChecked(Monday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickTuesday_YesOption()
        {
            WaitForElementToBeClickable(Tuesday_YesOption);
            Click(Tuesday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTuesday_YesOptionChecked()
        {
            WaitForElement(Tuesday_YesOption);
            ValidateElementChecked(Tuesday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTuesday_YesOptionNotChecked()
        {
            WaitForElement(Tuesday_YesOption);
            ValidateElementNotChecked(Tuesday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickTuesday_NoOption()
        {
            WaitForElementToBeClickable(Tuesday_NoOption);
            Click(Tuesday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTuesday_NoOptionChecked()
        {
            WaitForElement(Tuesday_NoOption);
            ValidateElementChecked(Tuesday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTuesday_NoOptionNotChecked()
        {
            WaitForElement(Tuesday_NoOption);
            ValidateElementNotChecked(Tuesday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickWednesday_YesOption()
        {
            WaitForElementToBeClickable(Wednesday_YesOption);
            Click(Wednesday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateWednesday_YesOptionChecked()
        {
            WaitForElement(Wednesday_YesOption);
            ValidateElementChecked(Wednesday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateWednesday_YesOptionNotChecked()
        {
            WaitForElement(Wednesday_YesOption);
            ValidateElementNotChecked(Wednesday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickWednesday_NoOption()
        {
            WaitForElementToBeClickable(Wednesday_NoOption);
            Click(Wednesday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateWednesday_NoOptionChecked()
        {
            WaitForElement(Wednesday_NoOption);
            ValidateElementChecked(Wednesday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateWednesday_NoOptionNotChecked()
        {
            WaitForElement(Wednesday_NoOption);
            ValidateElementNotChecked(Wednesday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickThursday_YesOption()
        {
            WaitForElementToBeClickable(Thursday_YesOption);
            Click(Thursday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateThursday_YesOptionChecked()
        {
            WaitForElement(Thursday_YesOption);
            ValidateElementChecked(Thursday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateThursday_YesOptionNotChecked()
        {
            WaitForElement(Thursday_YesOption);
            ValidateElementNotChecked(Thursday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickThursday_NoOption()
        {
            WaitForElementToBeClickable(Thursday_NoOption);
            Click(Thursday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateThursday_NoOptionChecked()
        {
            WaitForElement(Thursday_NoOption);
            ValidateElementChecked(Thursday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateThursday_NoOptionNotChecked()
        {
            WaitForElement(Thursday_NoOption);
            ValidateElementNotChecked(Thursday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickFriday_YesOption()
        {
            WaitForElementToBeClickable(Friday_YesOption);
            Click(Friday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateFriday_YesOptionChecked()
        {
            WaitForElement(Friday_YesOption);
            ValidateElementChecked(Friday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateFriday_YesOptionNotChecked()
        {
            WaitForElement(Friday_YesOption);
            ValidateElementNotChecked(Friday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickFriday_NoOption()
        {
            WaitForElementToBeClickable(Friday_NoOption);
            Click(Friday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateFriday_NoOptionChecked()
        {
            WaitForElement(Friday_NoOption);
            ValidateElementChecked(Friday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateFriday_NoOptionNotChecked()
        {
            WaitForElement(Friday_NoOption);
            ValidateElementNotChecked(Friday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickSaturday_YesOption()
        {
            WaitForElementToBeClickable(Saturday_YesOption);
            Click(Saturday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSaturday_YesOptionChecked()
        {
            WaitForElement(Saturday_YesOption);
            ValidateElementChecked(Saturday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSaturday_YesOptionNotChecked()
        {
            WaitForElement(Saturday_YesOption);
            ValidateElementNotChecked(Saturday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickSaturday_NoOption()
        {
            WaitForElementToBeClickable(Saturday_NoOption);
            Click(Saturday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSaturday_NoOptionChecked()
        {
            WaitForElement(Saturday_NoOption);
            ValidateElementChecked(Saturday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSaturday_NoOptionNotChecked()
        {
            WaitForElement(Saturday_NoOption);
            ValidateElementNotChecked(Saturday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickSunday_YesOption()
        {
            WaitForElementToBeClickable(Sunday_YesOption);
            Click(Sunday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSunday_YesOptionChecked()
        {
            WaitForElement(Sunday_YesOption);
            ValidateElementChecked(Sunday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSunday_YesOptionNotChecked()
        {
            WaitForElement(Sunday_YesOption);
            ValidateElementNotChecked(Sunday_YesOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ClickSunday_NoOption()
        {
            WaitForElementToBeClickable(Sunday_NoOption);
            Click(Sunday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSunday_NoOptionChecked()
        {
            WaitForElement(Sunday_NoOption);
            ValidateElementChecked(Sunday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateSunday_NoOptionNotChecked()
        {
            WaitForElement(Sunday_NoOption);
            ValidateElementNotChecked(Sunday_NoOption);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateNotesText(string ExpectedText)
        {
            ValidateElementText(NotesTextArea, ExpectedText);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage InsertTextOnNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(NotesTextArea);
            SendKeys(NotesTextArea, TextToInsert);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTimeBandStartFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(TimeBandStartField);
                MoveToElementInPage(TimeBandStartField);
            }

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(TimeBandStartField));
            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTimeBandEndFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(TimeBandEndField);
                MoveToElementInPage(TimeBandEndField);
            }

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(TimeBandEndField));
            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTimebandStartFieldErrorMessage(string ExpectedText)
        {
            WaitForElementVisible(TimeBandStartFieldError);
            ValidateElementByTitle(TimeBandStartFieldError, ExpectedText);

            return this;
        }

        public ServiceProvidedRateScheduleRecordPage ValidateTimebandEndFieldErrorMessage(string ExpectedText)
        {
            WaitForElementVisible(TimeBandEndFieldError);
            ValidateElementByTitle(TimeBandEndFieldError, ExpectedText);

            return this;
        }
    }
}

