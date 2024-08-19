using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CareProviderBookingScheduleRecordPage : CommonMethods
    {
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CWDialogIFrame = By.XPath("//*[starts-with(@id, 'iframe_CWDialog_')][contains(@src, 'type=cpbookingschedule&')]");
        //readonly By DataFormDialogIFrame = By.XPath("//*[@id='iframe_CWDataFormDialog']");

        readonly By CWIFrame_Staff = By.Id("CWIFrame_Staff");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
        readonly By LocationidLink = By.XPath("//*[@id='CWField_locationid_Link']");
        readonly By LocationidClearButton = By.XPath("//*[@id='CWClearLookup_locationid']");
        readonly By LocationidLookupButton = By.XPath("//*[@id='CWLookupBtn_locationid']");
        readonly By BookingtypeidLink = By.XPath("//*[@id='CWField_bookingtypeid_Link']");
        readonly By BookingtypeidClearButton = By.XPath("//*[@id='CWClearLookup_bookingtypeid']");
        readonly By BookingtypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_bookingtypeid']");
        readonly By Startdayofweekid = By.XPath("//*[@id='CWField_startdayofweekid']");
        readonly By Enddayofweekid = By.XPath("//*[@id='CWField_enddayofweekid']");
        readonly By Isdeleted_1 = By.XPath("//*[@id='CWField_isdeleted_1']");
        readonly By Isdeleted_0 = By.XPath("//*[@id='CWField_isdeleted_0']");
        readonly By Genderpreferenceid = By.XPath("//*[@id='CWField_genderpreferenceid']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By Starttime = By.XPath("//*[@id='CWField_starttime']");
        readonly By Starttime_TimePicker = By.XPath("//*[@id='CWField_starttime_TimePicker']");
        readonly By Endtime = By.XPath("//*[@id='CWField_endtime']");
        readonly By Endtime_TimePicker = By.XPath("//*[@id='CWField_endtime_TimePicker']");
        readonly By Deletioncomments = By.XPath("//*[@id='CWField_deletioncomments']");
        readonly By CareproviderserviceidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderserviceid']");
        readonly By Comments = By.XPath("//*[@id='CWField_comments']");
        readonly By Frequency = By.XPath("//*[@id='CWField_frequency']");
        readonly By Nextoccurrencedate = By.XPath("//*[@id='CWField_nextoccurrencedate']");
        readonly By NextoccurrencedateDatePicker = By.XPath("//*[@id='CWField_nextoccurrencedate_DatePicker']");
        readonly By Firstoccurrencedate = By.XPath("//*[@id='CWField_firstoccurrencedate']");
        readonly By FirstoccurrencedateDatePicker = By.XPath("//*[@id='CWField_firstoccurrencedate_DatePicker']");
        readonly By Expressbookonpublicholidaydefaultid = By.XPath("//*[@id='CWField_expressbookonpublicholidaydefaultid']");
        readonly By Lastoccurrencedate = By.XPath("//*[@id='CWField_lastoccurrencedate']");
        readonly By LastoccurrencedateDatePicker = By.XPath("//*[@id='CWField_lastoccurrencedate_DatePicker']");  
        
        readonly By PageTitle = By.XPath("//h1[@class='page-title']");
        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

        readonly By StaffSectionHeader = By.XPath("//*[@id = 'CWSection_BookingScheduleStaff']");
        readonly By Staff_CWIFrame = By.Id("CWIFrame_Staff");
        readonly By BookingScheduleStaffPageTitle = By.XPath("//h1[@class='page-title'][text() = 'Booking Schedule Staff']");

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By auditHistory = By.Id("CWNavItem_AuditHistory");

        By BookingScheduleStaffRecord(string RecordID) => By.XPath("//tr[@id='"+ RecordID +"']");
        By BookingScheduleStaffRecordCellContent(string RecordID, int CellNumber) => By.XPath("//tr[@id='" + RecordID + "']/td["+ CellNumber + "']");

        public CareProviderBookingScheduleRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        public CareProviderBookingScheduleRecordPage WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWDialogIFrame);
            SwitchToIframe(CWDialogIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(PageTitle);
            WaitForElement(DetailsTab);

            return this;
        }

        public CareProviderBookingScheduleRecordPage WaitForStaffSectionToLoad()
        {
            WaitForElement(StaffSectionHeader);
            ScrollToElement(StaffSectionHeader);
            
            WaitForElement(Staff_CWIFrame);
            ScrollToElement(Staff_CWIFrame);
            SwitchToIframe(Staff_CWIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(BookingScheduleStaffPageTitle);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            ScrollToElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickLocationidLink()
        {
            WaitForElementToBeClickable(LocationidLink);
            Click(LocationidLink);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateLocationidLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(LocationidLink);
            ValidateElementText(LocationidLink, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickLocationidClearButton()
        {
            WaitForElementToBeClickable(LocationidClearButton);
            Click(LocationidClearButton);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickLocationidLookupButton()
        {
            WaitForElementToBeClickable(LocationidLookupButton);
            Click(LocationidLookupButton);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickBookingtypeidLink()
        {
            WaitForElementToBeClickable(BookingtypeidLink);
            Click(BookingtypeidLink);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateBookingtypeidLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(BookingtypeidLink);
            ValidateElementText(BookingtypeidLink, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickBookingtypeidClearButton()
        {
            WaitForElementToBeClickable(BookingtypeidClearButton);
            Click(BookingtypeidClearButton);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickBookingtypeidLookupButton()
        {
            WaitForElementToBeClickable(BookingtypeidLookupButton);
            Click(BookingtypeidLookupButton);

            return this;
        }

        public CareProviderBookingScheduleRecordPage SelectStartDayOfWeek(string TextToSelect)
        {
            WaitForElementToBeClickable(Startdayofweekid);
            SelectPicklistElementByText(Startdayofweekid, TextToSelect);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateStartdayofweekidSelectedText(string ExpectedText)
        {
            ValidateElementText(Startdayofweekid, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage SelectEndDayOfWeek(string TextToSelect)
        {
            WaitForElementToBeClickable(Enddayofweekid);
            SelectPicklistElementByText(Enddayofweekid, TextToSelect);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateEnddayofweekidSelectedText(string ExpectedText)
        {
            ValidateElementText(Enddayofweekid, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickIsdeleted_YesOption()
        {
            WaitForElementToBeClickable(Isdeleted_1);
            Click(Isdeleted_1);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateIsdeleted_YesOptionChecked()
        {
            WaitForElement(Isdeleted_1);
            ScrollToElement(Isdeleted_1);
            ValidateElementChecked(Isdeleted_1);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateIsdeleted_YesOptionNotChecked()
        {
            WaitForElement(Isdeleted_1);
            ScrollToElement(Isdeleted_1);
            ValidateElementNotChecked(Isdeleted_1);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickIsdeleted_NoOption()
        {
            WaitForElementToBeClickable(Isdeleted_0);
            Click(Isdeleted_0);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateIsdeleted_NoOptionChecked()
        {
            WaitForElement(Isdeleted_0);
            ScrollToElement(Isdeleted_0);
            ValidateElementChecked(Isdeleted_0);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateIsdeleted_NoOptionNotChecked()
        {
            WaitForElement(Isdeleted_0);
            ScrollToElement(Isdeleted_0);
            ValidateElementNotChecked(Isdeleted_0);

            return this;
        }

        public CareProviderBookingScheduleRecordPage SelectGenderPreference(string TextToSelect)
        {
            WaitForElementToBeClickable(Genderpreferenceid);
            SelectPicklistElementByText(Genderpreferenceid, TextToSelect);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateGenderPreferenceSelectedText(string ExpectedText)
        {
            ValidateElementText(Genderpreferenceid, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateStartTimeText(string ExpectedText)
        {
            ValidateElementValue(Starttime, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage InsertTextOnStartTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Starttime);
            SendKeys(Starttime, TextToInsert);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickStartTime_TimePicker()
        {
            WaitForElementToBeClickable(Starttime_TimePicker);
            Click(Starttime_TimePicker);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateEndTimeText(string ExpectedText)
        {
            ValidateElementValue(Endtime, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage InsertTextOnEndTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Endtime);
            SendKeys(Endtime, TextToInsert);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickEndTime_TimePicker()
        {
            WaitForElementToBeClickable(Endtime_TimePicker);
            Click(Endtime_TimePicker);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateDeletionCommentsText(string ExpectedText)
        {
            ValidateElementText(Deletioncomments, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage InsertTextOnDeletionComments(string TextToInsert)
        {
            WaitForElementToBeClickable(Deletioncomments);
            SendKeys(Deletioncomments, TextToInsert);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickCareproviderServiceLookupButton()
        {
            WaitForElementToBeClickable(CareproviderserviceidLookupButton);
            Click(CareproviderserviceidLookupButton);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateCommentsText(string ExpectedText)
        {
            ScrollToElement(Comments);
            ValidateElementText(Comments, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage InsertTextOnComments(string TextToInsert)
        {
            WaitForElementToBeClickable(Comments);
            SendKeys(Comments, TextToInsert);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateFrequencyText(string ExpectedText)
        {
            ValidateElementValue(Frequency, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage InsertTextOnFrequency(string TextToInsert)
        {
            WaitForElementToBeClickable(Frequency);
            SendKeys(Frequency, TextToInsert);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateNextoccurrencedateText(string ExpectedText)
        {
            ValidateElementValue(Nextoccurrencedate, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage InsertTextOnNextOccurrenceDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Nextoccurrencedate);
            Click(Nextoccurrencedate);
            SendKeys(Nextoccurrencedate, TextToInsert);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickNextoccurrencedateDatePicker()
        {
            WaitForElementToBeClickable(NextoccurrencedateDatePicker);
            Click(NextoccurrencedateDatePicker);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateFirstoccurrencedateText(string ExpectedText)
        {
            ValidateElementValue(Firstoccurrencedate, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage InsertTextOnFirstOccurrenceDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Firstoccurrencedate);
            ScrollToElement(Firstoccurrencedate);
            Click(Firstoccurrencedate);
            SendKeys(Firstoccurrencedate, TextToInsert);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickFirstOccurrenceDateDatePicker()
        {
            WaitForElementToBeClickable(FirstoccurrencedateDatePicker);
            ScrollToElement(FirstoccurrencedateDatePicker);
            Click(FirstoccurrencedateDatePicker);

            return this;
        }

        public CareProviderBookingScheduleRecordPage SelectExpressbookonpublicholidaydefaultid(string TextToSelect)
        {
            WaitForElementToBeClickable(Expressbookonpublicholidaydefaultid);
            SelectPicklistElementByText(Expressbookonpublicholidaydefaultid, TextToSelect);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateExpressbookonpublicholidaydefaultidSelectedText(string ExpectedText)
        {
            ValidateElementText(Expressbookonpublicholidaydefaultid, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateLastOccurrenceDateText(string ExpectedText)
        {
            ValidateElementValue(Lastoccurrencedate, ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage InsertTextOnLastOccurrenceDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Lastoccurrencedate);
            ScrollToElement(Lastoccurrencedate);
            Click(Lastoccurrencedate);
            SendKeys(Lastoccurrencedate, TextToInsert);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ClickLastOccurrenceDateDatePicker()
        {
            WaitForElementToBeClickable(LastoccurrencedateDatePicker);
            ScrollToElement(LastoccurrencedateDatePicker);
            Click(LastoccurrencedateDatePicker);

            return this;
        }

        public CareProviderBookingScheduleRecordPage OpenBookingScheduleStaffRecord(string cpBookingScheduleStaffRecordId)
        {
            WaitForElementToBeClickable(BookingScheduleStaffRecord(cpBookingScheduleStaffRecordId));
            ScrollToElement(BookingScheduleStaffRecord(cpBookingScheduleStaffRecordId));
            Click(BookingScheduleStaffRecord(cpBookingScheduleStaffRecordId));

            return this;
        }

        public CareProviderBookingScheduleRecordPage OpenBookingScheduleStaffRecord(Guid cpBookingScheduleStaffRecordId)
        {
            OpenBookingScheduleStaffRecord(cpBookingScheduleStaffRecordId.ToString());

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateBookingScheduleStaffRecordIsDisplayed(string cpBookingScheduleStaffRecordId, bool IsDisplayed = true)
        {
            if (IsDisplayed)
                WaitForElement(BookingScheduleStaffRecord(cpBookingScheduleStaffRecordId));
            else
                WaitForElementNotVisible(BookingScheduleStaffRecord(cpBookingScheduleStaffRecordId), 3);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateBookingScheduleStaffRecordIsDisplayed(Guid cpBookingScheduleStaffRecordId, bool IsDisplayed = true)
        {
            ValidateBookingScheduleStaffRecordIsDisplayed(cpBookingScheduleStaffRecordId.ToString(), IsDisplayed);

            return this;
        }

        public CareProviderBookingScheduleRecordPage ValidateBookingScheduleStaffRecordCellContent(string cpBookingScheduleStaffRecordId, int CellNumber, string ExpectedText)
        {
            WaitForElementToBeClickable(BookingScheduleStaffRecordCellContent(cpBookingScheduleStaffRecordId, CellNumber));
            ScrollToElement(BookingScheduleStaffRecordCellContent(cpBookingScheduleStaffRecordId, CellNumber));
            ValidateElementByTitle(BookingScheduleStaffRecordCellContent(cpBookingScheduleStaffRecordId, CellNumber), ExpectedText);

            return this;
        }

        public CareProviderBookingScheduleRecordPage NavigateToRelatedItemsSubPage_Audit()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(auditHistory);
            Click(auditHistory);

            return this;
        }
    }
}
