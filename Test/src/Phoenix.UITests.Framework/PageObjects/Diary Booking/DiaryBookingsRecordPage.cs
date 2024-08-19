using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DiaryBookingsRecordPage : CommonMethods
    {
        public DiaryBookingsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By DiaryPageRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbookingdiary&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By BookingDiaryStaffIFrame = By.Id("CWIFrame_Staff");


        readonly By DiaryBookingPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Diary Bookings']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        By recordCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']");

        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.XPath("//button[@id='CWRefreshButton']");
        readonly By backButton = By.Id("BackButton");

        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        By DiaryBookingStaffRecord(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']");


        public DiaryBookingsRecordPage WaitForDiaryBookingRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(DiaryPageRecordIFrame);
            SwitchToIframe(DiaryPageRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public DiaryBookingsRecordPage WaitForDiaryBookingRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(DiaryPageRecordIFrame);
            SwitchToIframe(DiaryPageRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public DiaryBookingsRecordPage WaitForBookingDiaryStaffSubRecordPageToLoad()
        {

            WaitForElement(BookingDiaryStaffIFrame);
            SwitchToIframe(BookingDiaryStaffIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public DiaryBookingsRecordPage ClickRecordCellText(string RecordID, int CellPosition)
        {
            WaitForElementToBeClickable(recordCell(RecordID, CellPosition));
            ScrollToElement(recordCell(RecordID, CellPosition));
            System.Threading.Thread.Sleep(2000);
            Click(recordCell(RecordID, CellPosition));

            return this;
        }

        public DiaryBookingsRecordPage ValidateRecordIsPresent(string RecordID, bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(recordCell(RecordID));
            else
                WaitForElementNotVisible(recordCell(RecordID), 3);

            return this;
        }

        public DiaryBookingsRecordPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordCell(RecordID, CellPosition));
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }


        public DiaryBookingsRecordPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public DiaryBookingsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            ScrollToElement(backButton);
            Click(backButton);

            return this;
        }

        public DiaryBookingsRecordPage OpenDiaryBookingStaffRecord(string cpDiaryBookingStaffRecordId)
        {
            WaitForElementToBeClickable(DiaryBookingStaffRecord(cpDiaryBookingStaffRecordId));
            ScrollToElement(DiaryBookingStaffRecord(cpDiaryBookingStaffRecordId));
            Click(DiaryBookingStaffRecord(cpDiaryBookingStaffRecordId));

            return this;
        }

        public DiaryBookingsRecordPage OpenDiaryBookingStaffRecord(Guid cpDiaryBookingStaffRecordId)
        {
            OpenDiaryBookingStaffRecord(cpDiaryBookingStaffRecordId.ToString());

            return this;
        }

        public DiaryBookingsRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

    }
}
