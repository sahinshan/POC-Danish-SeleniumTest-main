using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.Finance;

namespace Phoenix.UITests.Framework.PageObjects.ExpressBook
{
	public class ExpressBookingResultRecordPage : CommonMethods
	{
        public ExpressBookingResultRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id, iframe_CWDialog_)][contains(@src, 'editpage.aspx?type=cpexpressbookingresult&')]");

		readonly By RunOnDemandWorkflowButton = By.Id("TI_RunOnDemandWorkflow");
        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By detailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']");

        readonly By CopyRecordLink = By.XPath("//*[@id='TI_CopyRecordLink']");
        readonly By Expressbookingfailurereasonid = By.XPath("//*[@id='CWField_expressbookingfailurereasonid']");
        readonly By Dateofbooking = By.XPath("//*[@id='CWField_dateofbooking']");
        readonly By DateofbookingDatePicker = By.XPath("//*[@id='CWField_dateofbooking_DatePicker']");
        readonly By Starttime = By.XPath("//*[@id='CWField_starttime']");
        readonly By Starttime_TimePicker = By.XPath("//*[@id='CWField_starttime_TimePicker']");
        readonly By Providername = By.XPath("//*[@id='CWField_providername']");
        readonly By CpbookingscheduleidLink = By.XPath("//*[@id='CWField_cpbookingscheduleid_Link']");
        readonly By CpbookingscheduleidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingscheduleid']");
        readonly By Staffnames = By.XPath("//*[@id='CWField_staffnames']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By Finishtime = By.XPath("//*[@id='CWField_finishtime']");
        readonly By Finishtime_TimePicker = By.XPath("//*[@id='CWField_finishtime_TimePicker']");
        readonly By CpexpressbookingcriteriaidLink = By.XPath("//*[@id='CWField_cpexpressbookingcriteriaid_Link']");
        readonly By CpexpressbookingcriteriaidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpexpressbookingcriteriaid']");
        readonly By CpbookingdiaryidLink = By.XPath("//*[@id='CWField_cpbookingdiaryid_Link']");
        readonly By CpbookingdiaryidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingdiaryid']");
        readonly By Exceptionmessage = By.XPath("//*[@id='CWField_exceptionmessage']");
        By MandatoryField_Label(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']/span[@class = 'mandatory']");

        public ExpressBookingResultRecordPage WaitForExpressBookingResultRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(BackButton);
            WaitForElement(detailsTab);
			WaitForElement(RunOnDemandWorkflowButton);

            return this;
        }

        public ExpressBookingResultRecordPage WaitForExpressBookingResultRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(BackButton);
            WaitForElement(detailsTab);

            return this;
        }

        public ExpressBookingResultRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

        public ExpressBookingResultRecordPage ClickRunOnDemandWorkflowButton()
        {
            WaitForElementToBeClickable(RunOnDemandWorkflowButton);
            Click(RunOnDemandWorkflowButton);

            return this;
        }

        public ExpressBookingResultRecordPage ClickCopyRecordLink()
        {
            WaitForElementToBeClickable(CopyRecordLink);
            Click(CopyRecordLink);

            return this;
        }

        public ExpressBookingResultRecordPage SelectExpressBookingFailureReason(string TextToSelect)
        {
            WaitForElementToBeClickable(Expressbookingfailurereasonid);
            SelectPicklistElementByText(Expressbookingfailurereasonid, TextToSelect);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateExpressBookingFailureReasonSelectedText(string ExpectedText)
        {
            ScrollToElement(Expressbookingfailurereasonid);
            ValidatePicklistSelectedText(Expressbookingfailurereasonid, ExpectedText);

            return this;
        }

        //Validate Express Booking Failure Reason is disabled
        public ExpressBookingResultRecordPage ValidateExpressBookingExceptionReasonDisabled(bool ExpectedDisabled = true)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(Expressbookingfailurereasonid);
            else
                ValidateElementEnabled(Expressbookingfailurereasonid);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateDateOfBookingText(string ExpectedText)
        {
            ValidateElementValue(Dateofbooking, ExpectedText);

            return this;
        }

        //Validate Date of Booking is disabled
        public ExpressBookingResultRecordPage ValidateDateOfBookingDisabled(bool ExpectedDisabled = true)
        {
            if(ExpectedDisabled)
                ValidateElementDisabled(Dateofbooking);
            else
                ValidateElementEnabled(Dateofbooking);

            return this;
        }

        public ExpressBookingResultRecordPage InsertTextOnDateOfBooking(string TextToInsert)
        {
            WaitForElementToBeClickable(Dateofbooking);
            SendKeys(Dateofbooking, TextToInsert);

            return this;
        }

        public ExpressBookingResultRecordPage ClickDateOfBookingDatePicker()
        {
            WaitForElementToBeClickable(DateofbookingDatePicker);
            Click(DateofbookingDatePicker);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateStartTimeText(string ExpectedText)
        {
            ValidateElementValue(Starttime, ExpectedText);

            return this;
        }

        //Validate Start Time is disabled
        public ExpressBookingResultRecordPage ValidateStartTimeDisabled(bool ExpectedDisabled = true)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(Starttime);
            else
                ValidateElementEnabled(Starttime);

            return this;
        }

        public ExpressBookingResultRecordPage InsertTextOnStartTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Starttime);
            SendKeys(Starttime, TextToInsert);

            return this;
        }

        public ExpressBookingResultRecordPage ClickStartTime_TimePicker()
        {
            WaitForElementToBeClickable(Starttime_TimePicker);
            Click(Starttime_TimePicker);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateProviderNameText(string ExpectedText)
        {
            ScrollToElement(Providername);
            ValidateElementValue(Providername, ExpectedText);

            return this;
        }

        //Validate Provider Name is disabled
        public ExpressBookingResultRecordPage ValidateProviderNameDisabled(bool ExpectedDisabled = true)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(Providername);
            else
                ValidateElementEnabled(Providername);

            return this;
        }

        public ExpressBookingResultRecordPage InsertTextOnProviderName(string TextToInsert)
        {
            WaitForElementToBeClickable(Providername);
            SendKeys(Providername, TextToInsert);

            return this;
        }

        public ExpressBookingResultRecordPage ClickBookingScheduleLink()
        {
            WaitForElementToBeClickable(CpbookingscheduleidLink);
            Click(CpbookingscheduleidLink);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateBookingScheduleLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CpbookingscheduleidLink);
            ValidateElementText(CpbookingscheduleidLink, ExpectedText);

            return this;
        }

        public ExpressBookingResultRecordPage ClickBookingScheduleLookupButton()
        {
            WaitForElementToBeClickable(CpbookingscheduleidLookupButton);
            Click(CpbookingscheduleidLookupButton);

            return this;
        }

        //validate schedule booking lookup button is visible
        public ExpressBookingResultRecordPage ValidateBookingScheduleLookupButtonVisible(bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
                WaitForElementVisible(CpbookingscheduleidLookupButton);
            else
                WaitForElementNotVisible(CpbookingscheduleidLookupButton, 2);

            return this;
        }

        //validate schedule booking lookup button is disabled
        public ExpressBookingResultRecordPage ValidateBookingScheduleLookupButtonDisabled(bool ExpectedDisabled = true)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(CpbookingscheduleidLookupButton);
            else
                ValidateElementEnabled(CpbookingscheduleidLookupButton);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateStaffNamesText(string ExpectedText)
        {
            ValidateElementValue(Staffnames, ExpectedText);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateStaffNamesContainsText(string ExpectedText)
        {
            var elementValue = GetElementByAttributeValue(Staffnames, "value");
            Assert.IsTrue(elementValue.Contains(ExpectedText), "The element value does not contain the text." + ExpectedText);

            return this;
        }

        //Validate Staff Names is disabled
        public ExpressBookingResultRecordPage ValidateStaffNamesDisabled(bool ExpectedDisabled = true)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(Staffnames);
            else
                ValidateElementEnabled(Staffnames);

            return this;
        }   

        public ExpressBookingResultRecordPage InsertTextOnStaffNames(string TextToInsert)
        {
            WaitForElementToBeClickable(Staffnames);
            SendKeys(Staffnames, TextToInsert);

            return this;
        }

        public ExpressBookingResultRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public ExpressBookingResultRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateFinishTimeText(string ExpectedText)
        {
            ValidateElementValue(Finishtime, ExpectedText);

            return this;
        }

        //Validate Finish Time is disabled
        public ExpressBookingResultRecordPage ValidateFinishTimeDisabled(bool ExpectedDisabled = true)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(Finishtime);
            else
                ValidateElementEnabled(Finishtime);

            return this;
        }

        public ExpressBookingResultRecordPage InsertTextOnFinishTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Finishtime);
            SendKeys(Finishtime, TextToInsert);

            return this;
        }

        public ExpressBookingResultRecordPage ClickFinishTime_TimePicker()
        {
            WaitForElementToBeClickable(Finishtime_TimePicker);
            Click(Finishtime_TimePicker);

            return this;
        }

        public ExpressBookingResultRecordPage ClickExpressBookingCriteriaLink()
        {
            WaitForElementToBeClickable(CpexpressbookingcriteriaidLink);
            Click(CpexpressbookingcriteriaidLink);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateExpressBookingCriteriaLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CpexpressbookingcriteriaidLink);
            ValidateElementText(CpexpressbookingcriteriaidLink, ExpectedText);

            return this;
        }

        public ExpressBookingResultRecordPage ClickExpressBookingCriteriaLookupButton()
        {
            WaitForElementToBeClickable(CpexpressbookingcriteriaidLookupButton);
            Click(CpexpressbookingcriteriaidLookupButton);

            return this;
        }

        public ExpressBookingResultRecordPage ClickBookingDiaryLink()
        {
            WaitForElementToBeClickable(CpbookingdiaryidLink);
            ScrollToElement(CpbookingdiaryidLink);
            Click(CpbookingdiaryidLink);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateBookingDiaryLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CpbookingdiaryidLink);
            ValidateElementText(CpbookingdiaryidLink, ExpectedText);

            return this;
        }

        public ExpressBookingResultRecordPage ClickBookingDiaryLookupButton()
        {
            WaitForElementToBeClickable(CpbookingdiaryidLookupButton);
            Click(CpbookingdiaryidLookupButton);

            return this;
        }

        //validate diary booking lookup button is visible
        public ExpressBookingResultRecordPage ValidateBookingDiaryLookupButtonVisible(bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
                WaitForElementVisible(CpbookingdiaryidLookupButton);
            else
                WaitForElementNotVisible(CpbookingdiaryidLookupButton, 2);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateExceptionMessageText(string ExpectedText)
        {
            WaitForElement(Exceptionmessage);
            ScrollToElement(Exceptionmessage);
            ValidateElementValue(Exceptionmessage, ExpectedText);

            return this;
        }

        //Validate Exception Message is disabled
        public ExpressBookingResultRecordPage ValidateExceptionMessageDisabled(bool ExpectedDisabled = true)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(Exceptionmessage);
            else
                ValidateElementEnabled(Exceptionmessage);

            return this;
        }

        public ExpressBookingResultRecordPage InsertTextOnExceptionMessage(string TextToInsert)
        {
            WaitForElementToBeClickable(Exceptionmessage);
            SendKeys(Exceptionmessage, TextToInsert);

            return this;
        }

        public ExpressBookingResultRecordPage ValidateMandatoryFieldIsDisplayed(string FieldName, bool ExpectedMandatory = true)
        {
            if (ExpectedMandatory)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 2);

            return this;
        }
    }
}
