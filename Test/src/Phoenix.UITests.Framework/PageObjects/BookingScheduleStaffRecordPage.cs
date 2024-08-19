using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class BookingScheduleStaffRecordPage : CommonMethods
    {
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CWDialogIFrame = By.XPath("//*[starts-with(@id, 'iframe_CWDialog_')][contains(@src, 'type=cpbookingschedulestaff&')]");
        readonly By PageTitle = By.XPath("//h1[@class='page-title']");
        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By RunOnDemandWorkflow = By.XPath("//*[@id='TI_RunOnDemandWorkflow']");
        readonly By CpbookingscheduleidLink = By.XPath("//*[@id='CWField_cpbookingscheduleid_Link']");
        readonly By CpbookingscheduleidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingscheduleid']");
        readonly By SystemuseremploymentcontractidLink = By.XPath("//*[@id='CWField_systemuseremploymentcontractid_Link']");
        readonly By SystemuseremploymentcontractidClearButton = By.XPath("//*[@id='CWClearLookup_systemuseremploymentcontractid']");
        readonly By SystemuseremploymentcontractidLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuseremploymentcontractid']");
        readonly By SystemuseridLink = By.XPath("//*[@id='CWField_systemuserid_Link']");
        readonly By SystemuseridLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuserid']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

        public BookingScheduleStaffRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        public BookingScheduleStaffRecordPage WaitForBookingScheduleStaffRecordPageToLoad()
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

            WaitForElement(SystemuseridLink);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickRunOnDemandWorkflow()
        {
            WaitForElementToBeClickable(RunOnDemandWorkflow);
            Click(RunOnDemandWorkflow);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickCpbookingscheduleidLink()
        {
            WaitForElementToBeClickable(CpbookingscheduleidLink);
            Click(CpbookingscheduleidLink);

            return this;
        }

        public BookingScheduleStaffRecordPage ValidateCpbookingscheduleidLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CpbookingscheduleidLink);
            ValidateElementText(CpbookingscheduleidLink, ExpectedText);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickCpbookingscheduleidLookupButton()
        {
            WaitForElementToBeClickable(CpbookingscheduleidLookupButton);
            Click(CpbookingscheduleidLookupButton);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickSystemuseremploymentcontractidLink()
        {
            WaitForElementToBeClickable(SystemuseremploymentcontractidLink);
            Click(SystemuseremploymentcontractidLink);

            return this;
        }

        public BookingScheduleStaffRecordPage ValidateSystemuseremploymentcontractidLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(SystemuseremploymentcontractidLink);
            ValidateElementText(SystemuseremploymentcontractidLink, ExpectedText);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickSystemuseremploymentcontractidClearButton()
        {
            WaitForElementToBeClickable(SystemuseremploymentcontractidClearButton);
            Click(SystemuseremploymentcontractidClearButton);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickSystemuseremploymentcontractidLookupButton()
        {
            WaitForElementToBeClickable(SystemuseremploymentcontractidLookupButton);
            Click(SystemuseremploymentcontractidLookupButton);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickRosteredEmployeeFieldLink()
        {
            WaitForElementToBeClickable(SystemuseridLink);
            Click(SystemuseridLink);

            return this;
        }

        public BookingScheduleStaffRecordPage ValidateRosteredEmployeeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(SystemuseridLink);
            ValidateElementText(SystemuseridLink, ExpectedText);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickRosteredEmployeeLookupButton()
        {
            WaitForElementToBeClickable(SystemuseridLookupButton);
            Click(SystemuseridLookupButton);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public BookingScheduleStaffRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public BookingScheduleStaffRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

    }
}
