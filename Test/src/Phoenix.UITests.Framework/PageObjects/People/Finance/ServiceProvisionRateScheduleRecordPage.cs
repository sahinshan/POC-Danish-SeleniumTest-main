using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class ServiceProvisionRateScheduleRecordPage : CommonMethods
    {
        public ServiceProvisionRateScheduleRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ServiceProvisionRateScheduleRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovisionrateschedule&')]");
        readonly By pageHeader = By.XPath("//h1");
        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By DeleteButton = By.XPath("//button[@title = 'Delete']");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By ServiceProvisionRatePeriodLink = By.XPath("//*[@id='CWField_serviceprovisionrateperiodid_Link']");
        readonly By ServiceProvisionRatePeriodLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceprovisionrateperiodid']");
        readonly By Rate = By.XPath("//*[@id='CWField_rate']");
        readonly By RateBankHolidayField = By.Id("CWField_ratebankholiday");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ServiceProvisionLink = By.XPath("//*[@id='CWField_serviceprovisionid_Link']");
        readonly By ServiceProvisionLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceprovisionid']");
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

        readonly By TimeBandStart_MandatoryLabel = By.XPath("//li[@id='CWLabelHolder_timebandstart']/label[text()='Timeband Start']/Span[text()='*']");
        readonly By TimeBandStart_Field = By.Id("CWField_timebandstart");
        readonly By TimeBandStart_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_timebandstart']/label/span");

        readonly By TimeBandEnd_MandatoryLabel = By.XPath("//li[@id='CWLabelHolder_timebandend']/label[text()='Timeband End']/Span[text()='*']");
        readonly By TimeBandEnd_Field = By.Id("CWField_timebandend");
        readonly By TimeBandEnd_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_timebandend']/label/span");

        public ServiceProvisionRateScheduleRecordPage WaitForServiceProvisionRateScheduleRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ServiceProvisionRateScheduleRecordIFrame);
            SwitchToIframe(ServiceProvisionRateScheduleRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElement(pageHeader);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            MoveToElementInPage(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            MoveToElementInPage(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteButton);
            MoveToElementInPage(DeleteButton);
            Click(DeleteButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 50);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickServiceProvisionRatePeriodidLink()
        {
            WaitForElementToBeClickable(ServiceProvisionRatePeriodLink);
            Click(ServiceProvisionRatePeriodLink);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateServiceProvisionRatePeriodLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ServiceProvisionRatePeriodLink);
            ValidateElementText(ServiceProvisionRatePeriodLink, ExpectedText);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickServiceProvisionRatePeriodLookupButton()
        {
            WaitForElementToBeClickable(ServiceProvisionRatePeriodLookupButton);
            Click(ServiceProvisionRatePeriodLookupButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateServiceProvisionRatePeriodLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(ServiceProvisionRatePeriodLookupButton);
            else
                ValidateElementNotDisabled(ServiceProvisionRatePeriodLookupButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateRateText(string ExpectedText)
        {
            WaitForElementVisible(Rate);
            ValidateElementValue(Rate, ExpectedText);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateRateFieldDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                WaitForElementToBeDisable(Rate);
                ValidateElementDisabled(Rate);
            }
            else
                ValidateElementNotDisabled(Rate);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateRateBankHolidayText(string ExpectedText)
        {
            WaitForElementVisible(RateBankHolidayField);
            ValidateElementValue(RateBankHolidayField, ExpectedText);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateRateBankHolidayFieldDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                WaitForElementToBeDisable(RateBankHolidayField);
                ValidateElementDisabled(RateBankHolidayField);
            }
            else
                ValidateElementNotDisabled(RateBankHolidayField);
            return this;
        }

        public ServiceProvisionRateScheduleRecordPage InsertRate(string TextToInsert)
        {
            WaitForElementToBeClickable(Rate);
            MoveToElementInPage(Rate);
            SendKeys(Rate, TextToInsert);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage InsertRateBankHoliday(string TextToInsert)
        {
            WaitForElementToBeClickable(RateBankHolidayField);
            MoveToElementInPage(RateBankHolidayField);
            SendKeys(RateBankHolidayField, TextToInsert);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickResponsibleTeamClearButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamClearButton);
            Click(ResponsibleTeamClearButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateResponsibleTeamLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(ResponsibleTeamLookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeamLookupButton);
            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickServiceProvisionLink()
        {
            WaitForElementToBeClickable(ServiceProvisionLink);
            Click(ServiceProvisionLink);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateServiceProvisionidLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ServiceProvisionLink);
            ValidateElementText(ServiceProvisionLink, ExpectedText);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickServiceProvisionLookupButton()
        {
            WaitForElementToBeClickable(ServiceProvisionLookupButton);
            Click(ServiceProvisionLookupButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateServiceProvisionLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(ServiceProvisionLookupButton);
            else
                ValidateElementNotDisabled(ServiceProvisionLookupButton);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickSelectall_YesOption()
        {
            WaitForElementToBeClickable(Selectall_YesOption);
            Click(Selectall_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSelectall_YesOptionChecked()
        {
            WaitForElementToBeClickable(Selectall_YesOption);
            ValidateElementChecked(Selectall_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSelectall_YesOptionNotChecked()
        {
            WaitForElementToBeClickable(Selectall_YesOption);
            ValidateElementNotChecked(Selectall_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickSelectall_NoOption()
        {
            WaitForElementToBeClickable(Selectall_NoOption);
            Click(Selectall_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSelectall_NoOptionChecked()
        {
            WaitForElementToBeClickable(Selectall_NoOption);
            ValidateElementChecked(Selectall_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSelectall_NoOptionNotChecked()
        {
            WaitForElementToBeClickable(Selectall_NoOption);
            ValidateElementNotChecked(Selectall_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSelectAllOptionsDisabled(bool ExpectedDisabled)
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

        public ServiceProvisionRateScheduleRecordPage ValidateMondayOptionsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Monday_YesOption);
                ValidateElementDisabled(Monday_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(Monday_YesOption);
                ValidateElementNotDisabled(Monday_NoOption);
            }

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateTuesdayOptionsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Tuesday_YesOption);
                ValidateElementDisabled(Tuesday_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(Tuesday_YesOption);
                ValidateElementNotDisabled(Tuesday_NoOption);
            }

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateWednesdayOptionsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Wednesday_YesOption);
                ValidateElementDisabled(Wednesday_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(Wednesday_YesOption);
                ValidateElementNotDisabled(Wednesday_NoOption);
            }

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateThursdayOptionsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Thursday_YesOption);
                ValidateElementDisabled(Thursday_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(Thursday_YesOption);
                ValidateElementNotDisabled(Thursday_NoOption);
            }

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateFridayOptionsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Friday_YesOption);
                ValidateElementDisabled(Friday_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(Friday_YesOption);
                ValidateElementNotDisabled(Friday_NoOption);
            }

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSaturdayOptionsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Saturday_YesOption);
                ValidateElementDisabled(Saturday_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(Saturday_YesOption);
                ValidateElementNotDisabled(Saturday_NoOption);
            }

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSundayOptionsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Sunday_YesOption);
                ValidateElementDisabled(Sunday_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(Sunday_YesOption);
                ValidateElementNotDisabled(Sunday_NoOption);
            }

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickMonday_YesOption()
        {
            WaitForElementToBeClickable(Monday_YesOption);
            MoveToElementInPage(Monday_YesOption);
            Click(Monday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateMonday_YesOptionChecked()
        {
            WaitForElementToBeClickable(Monday_YesOption);
            ValidateElementChecked(Monday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateMonday_YesOptionNotChecked()
        {
            WaitForElementToBeClickable(Monday_YesOption);
            ValidateElementNotChecked(Monday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickMonday_NoOption()
        {
            WaitForElementToBeClickable(Monday_NoOption);
            Click(Monday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateMonday_NoOptionChecked()
        {
            WaitForElementToBeClickable(Monday_NoOption);
            ValidateElementChecked(Monday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateMonday_NoOptionNotChecked()
        {
            WaitForElementToBeClickable(Monday_NoOption);
            ValidateElementNotChecked(Monday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickTuesday_YesOption()
        {
            WaitForElementToBeClickable(Tuesday_YesOption);
            Click(Tuesday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateTuesday_YesOptionChecked()
        {
            WaitForElementToBeClickable(Tuesday_YesOption);
            ValidateElementChecked(Tuesday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateTuesday_YesOptionNotChecked()
        {
            WaitForElementToBeClickable(Tuesday_YesOption);
            ValidateElementNotChecked(Tuesday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickTuesday_NoOption()
        {
            WaitForElementToBeClickable(Tuesday_NoOption);
            Click(Tuesday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateTuesday_NoOptionChecked()
        {
            WaitForElementToBeClickable(Tuesday_NoOption);
            ValidateElementChecked(Tuesday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateTuesday_NoOptionNotChecked()
        {
            WaitForElementToBeClickable(Tuesday_NoOption);
            ValidateElementNotChecked(Tuesday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickWednesday_YesOption()
        {
            WaitForElementToBeClickable(Wednesday_YesOption);
            Click(Wednesday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateWednesday_YesOptionChecked()
        {
            WaitForElementToBeClickable(Wednesday_YesOption);
            ValidateElementChecked(Wednesday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateWednesday_YesOptionNotChecked()
        {
            WaitForElementToBeClickable(Wednesday_YesOption);
            ValidateElementNotChecked(Wednesday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickWednesday_NoOption()
        {
            WaitForElementToBeClickable(Wednesday_NoOption);
            Click(Wednesday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateWednesday_NoOptionChecked()
        {
            WaitForElementToBeClickable(Wednesday_NoOption);
            ValidateElementChecked(Wednesday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateWednesday_NoOptionNotChecked()
        {
            WaitForElementToBeClickable(Wednesday_NoOption);
            ValidateElementNotChecked(Wednesday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickThursday_YesOption()
        {
            WaitForElementToBeClickable(Thursday_YesOption);
            Click(Thursday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateThursday_YesOptionChecked()
        {
            WaitForElementToBeClickable(Thursday_YesOption);
            ValidateElementChecked(Thursday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateThursday_YesOptionNotChecked()
        {
            WaitForElementToBeClickable(Thursday_YesOption);
            ValidateElementNotChecked(Thursday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickThursday_NoOption()
        {
            WaitForElementToBeClickable(Thursday_NoOption);
            Click(Thursday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateThursday_NoOptionChecked()
        {
            WaitForElementToBeClickable(Thursday_NoOption);
            ValidateElementChecked(Thursday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateThursday_NoOptionNotChecked()
        {
            WaitForElementToBeClickable(Thursday_NoOption);
            ValidateElementNotChecked(Thursday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickFriday_YesOption()
        {
            WaitForElementToBeClickable(Friday_YesOption);
            Click(Friday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateFriday_YesOptionChecked()
        {
            WaitForElementToBeClickable(Friday_YesOption);
            ValidateElementChecked(Friday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateFriday_YesOptionNotChecked()
        {
            WaitForElementToBeClickable(Friday_YesOption);
            ValidateElementNotChecked(Friday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickFriday_NoOption()
        {
            WaitForElementToBeClickable(Friday_NoOption);
            Click(Friday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateFriday_NoOptionChecked()
        {
            WaitForElementToBeClickable(Friday_NoOption);
            ValidateElementChecked(Friday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateFriday_NoOptionNotChecked()
        {
            WaitForElementToBeClickable(Friday_NoOption);
            ValidateElementNotChecked(Friday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickSaturday_YesOption()
        {
            WaitForElementToBeClickable(Saturday_YesOption);
            Click(Saturday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSaturday_YesOptionChecked()
        {
            WaitForElementToBeClickable(Saturday_YesOption);
            ValidateElementChecked(Saturday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSaturday_YesOptionNotChecked()
        {
            WaitForElementToBeClickable(Saturday_YesOption);
            ValidateElementNotChecked(Saturday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickSaturday_NoOption()
        {
            WaitForElementToBeClickable(Saturday_NoOption);
            Click(Saturday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSaturday_NoOptionChecked()
        {
            WaitForElementToBeClickable(Saturday_NoOption);
            ValidateElementChecked(Saturday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSaturday_NoOptionNotChecked()
        {
            WaitForElementToBeClickable(Saturday_NoOption);
            ValidateElementNotChecked(Saturday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickSunday_YesOption()
        {
            WaitForElementToBeClickable(Sunday_YesOption);
            Click(Sunday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSunday_YesOptionChecked()
        {
            WaitForElementToBeClickable(Sunday_YesOption);
            ValidateElementChecked(Sunday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSunday_YesOptionNotChecked()
        {
            WaitForElementToBeClickable(Sunday_YesOption);
            ValidateElementNotChecked(Sunday_YesOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ClickSunday_NoOption()
        {
            WaitForElementToBeClickable(Sunday_NoOption);
            Click(Sunday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSunday_NoOptionChecked()
        {
            WaitForElementToBeClickable(Sunday_NoOption);
            ValidateElementChecked(Sunday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateSunday_NoOptionNotChecked()
        {
            WaitForElementToBeClickable(Sunday_NoOption);
            ValidateElementNotChecked(Sunday_NoOption);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateNotesText(string ExpectedText)
        {
            ValidateElementText(NotesTextArea, ExpectedText);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage InsertTextOnNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(NotesTextArea);
            SendKeys(NotesTextArea, TextToInsert);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateTimeBandStartValue(string ExpectedValue)
        {
            WaitForElementVisible(TimeBandStart_Field);
            ValidateElementValue(TimeBandStart_Field, ExpectedValue);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateTimeBandStartFieldVisibility(bool Visibility)
        {
            if (Visibility)
            {
                WaitForElementVisible(TimeBandStart_MandatoryLabel);
                WaitForElementVisible(TimeBandStart_Field);
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(TimeBandStart_MandatoryLabel));
                Assert.IsFalse(GetElementVisibility(TimeBandStart_Field));
            }

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateTimeBandEndValue(string ExpectedValue)
        {
            WaitForElementVisible(TimeBandEnd_Field);
            ValidateElementValue(TimeBandEnd_Field, ExpectedValue);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateTimeBandEndFieldVisibility(bool Visibility)
        {
            if (Visibility)
            {
                WaitForElementVisible(TimeBandEnd_MandatoryLabel);
                WaitForElementVisible(TimeBandEnd_Field);
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(TimeBandEnd_MandatoryLabel));
                Assert.IsFalse(GetElementVisibility(TimeBandEnd_Field));
            }

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage InsertTimeBandStart(string TextToInsert)
        {
            WaitForElement(TimeBandStart_Field);
            MoveToElementInPage(TimeBandStart_Field);
            SendKeys(TimeBandStart_Field, TextToInsert);
            SendKeysWithoutClearing(TimeBandStart_Field, Keys.Tab);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage InsertTimeBandEnd(string TextToInsert)
        {
            WaitForElement(TimeBandEnd_Field);
            MoveToElementInPage(TimeBandEnd_Field);
            SendKeys(TimeBandEnd_Field, TextToInsert);
            SendKeysWithoutClearing(TimeBandEnd_Field, Keys.Tab);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateTimeBandStartErrorLabelText(string ExpectedText)
        {
            ValidateElementText(TimeBandStart_FieldErrorLabel, ExpectedText);

            return this;
        }

        public ServiceProvisionRateScheduleRecordPage ValidateTimeBandEndErrorLabelText(string ExpectedText)
        {
            ValidateElementText(TimeBandEnd_FieldErrorLabel, ExpectedText);

            return this;
        }

    }
}

